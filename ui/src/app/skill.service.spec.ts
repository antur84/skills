import { Observable } from 'rxjs/Rx';
import { Http } from '@angular/http';
import { TestBed, inject } from '@angular/core/testing';
import { SkillService } from './skill.service';

describe('Service: Skill', () => {

  let httpMock: Http,
    sut: SkillService,
    postedRequest,
    skillViewModels: SkillViewModel[];

  beforeEach(() => {
    skillViewModels = [];
  });

  beforeEach(() => {
    httpMock = jasmine.createSpyObj('http', ['post', 'get']);

    (httpMock.post as jasmine.Spy).and.callFake((url, request) => {
      postedRequest = JSON.parse(request);
      return Observable.from([{}]);
    });

    (httpMock.get as jasmine.Spy).and.callFake((url) => {
      let response = {
        json: () => {
          return { data: skillViewModels };
        }
      };
      return Observable.from([response]);
    });
  });

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        SkillService,
        { provide: Http, useValue: httpMock }]
    });
  });

  beforeEach(inject([SkillService], (_sut_: SkillService) => {
    sut = _sut_;
  }));

  describe('after injecting', () => {
    it('should define a service', () => {
      expect(sut).toBeTruthy();
    });

    describe('when calling add', () => {
      beforeEach(() => {
        sut.add('c#', 5);
      });

      it('should post to backend', () => {
        expect(httpMock.post).toHaveBeenCalled();
      });

      it('should have have created a guid in request', () => {
        expect(postedRequest.externalId).not.toBeNull();
      });

      it('should have set the name in request', () => {
        expect(postedRequest.name).toBe('c#');
      });

      it('should have set the rating in request', () => {
        expect(postedRequest.rating).toBe(5);
      });

      it('should have added to list', () => {
        sut.getAll().subscribe(x => {
          expect(x.length).toBe(1);
          expect(x[0].name).toBe('c#');
        });
      });
    });
  });
});
