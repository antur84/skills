import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { UUID } from 'angular2-uuid';

// Import RxJs required methods
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class SkillService {

  private readonly skillsUrl = 'http://localhost:52752/api/skills';

  constructor(private http: Http) { }

  add(name: string, rating: number) {
    const skill = {
      externalId: UUID.UUID(),
      name: name,
      rating: rating
    };
    console.log("hallå?!!!");

    const body = JSON.stringify(skill);
    return this.http.post(this.skillsUrl, body, this.createOptions())
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
  }

  private createOptions() {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return options;
  }
}
