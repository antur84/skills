import { Observable } from 'rxjs/Rx';
import { SkillService } from './../skill.service';
import { StarBarComponent } from './../star-bar/star-bar.component';
/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { SkillListerComponent } from './skill-lister.component';
import { MaterialModule } from '@angular/material';

describe('SkillListerComponent', () => {
  let component: SkillListerComponent;
  let fixture: ComponentFixture<SkillListerComponent>;
  let skillServiceMock: SkillService;
  let skillViewModels: SkillViewModel[];

  beforeEach(() => {
    skillViewModels = [
      {
        externalId: '1',
        name: 'js',
        rating: 1
      },
      {
        externalId: '2',
        name: 'c#',
        rating: 5
      }
    ];
  });

  beforeEach(() => {
    skillServiceMock = jasmine.createSpyObj('skillService', ['getAll']);
    (skillServiceMock.getAll as jasmine.Spy).and.callFake(() => {
      return Observable.from([skillViewModels]);
    });
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule.forRoot()],
      declarations: [SkillListerComponent, StarBarComponent],
      providers: [
        { provide: SkillService, useValue: skillServiceMock }
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillListerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should list a line for each skill', () => {
    expect(fixture.debugElement.queryAll(By.css('app-star-bar')).length).toBe(skillViewModels.length);
  });
});
