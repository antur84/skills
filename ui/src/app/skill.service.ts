import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable, BehaviorSubject } from 'rxjs/Rx';
import { UUID } from 'angular2-uuid';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class SkillService {

  private skillViewModels: BehaviorSubject<SkillViewModel[]> = new BehaviorSubject([]);

  private readonly skillsUrl = 'http://localhost:52752/api/skills';

  constructor(private http: Http) {
    this.load();
  }

  getAll(): Observable<SkillViewModel[]> {
    return new Observable<SkillViewModel[]>(fn => this.skillViewModels.subscribe(fn));
  }

  add(name: string, rating: number) {
    const skillViewModel = this.createSkillViewModel(name, rating, UUID.UUID());
    const body = JSON.stringify(skillViewModel);
    const postObservable = this.http.post(this.skillsUrl, body, this.createOptions()).catch(this.handleErrorIfAny).share();
    postObservable.subscribe((res: Response) => {
      this.skillViewModels.next([skillViewModel].concat(this.skillViewModels.getValue()));
    }, (err) => err);
    return postObservable;
  }

  update(externalId: string, rating: number) {
    const current = this.skillViewModels.getValue().find(x => x.externalId === externalId);
    const body = JSON.stringify(this.createSkillViewModel(current.name, rating, externalId));
    const postObservable = this.http.put(this.skillsUrl, body, this.createOptions()).catch(this.handleErrorIfAny).share();
    postObservable.subscribe((res: Response) => {
      current.rating = rating;
      this.skillViewModels.next(this.skillViewModels.getValue());
    }, (err) => err);
    return postObservable;
  }

  delete(externalId: string) {
    const postObservable = this.http.delete(this.skillsUrl + '/' + externalId, this.createOptions())
      .catch(this.handleErrorIfAny).share();
    postObservable.subscribe((res: Response) => {
      let allExceptDeleted = this.skillViewModels.getValue().filter(x => x.externalId !== externalId);
      this.skillViewModels.next(allExceptDeleted);
    }, (err) => err);
    return postObservable;
  }

  private createSkillViewModel(name: string, rating: number, externalId: string) {
    return {
      externalId: externalId,
      name: name,
      rating: rating
    } as SkillViewModel;
  }

  private load() {
    return this.http.get(this.skillsUrl, this.createOptions()).subscribe(res => {
      this.skillViewModels.next(res.json().data);
    }, this.handleErrorIfAny);
  }

  private createOptions() {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return options;
  }

  private handleErrorIfAny(error: any) {
    return Observable.throw(error.json() || 'Unknown API error');
  }
}
