import { Observable } from 'rxjs/Rx';
import { StarBarComponent } from './../star-bar/star-bar.component';
import { SkillService } from './../skill.service';
/* tslint:disable:no-unused-variable */
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { MaterialModule, MdInput, MdButton } from '@angular/material';
import { SkillAdderComponent } from './skill-adder.component';

describe('SkillAdderComponent', () => {
  let component: SkillAdderComponent;
  let fixture: ComponentFixture<SkillAdderComponent>;
  let skillServiceMock: SkillService;

  beforeEach(() => {
    skillServiceMock = jasmine.createSpyObj('skillService', ['add']);
    (skillServiceMock.add as jasmine.Spy).and.callFake(() => {
      return Observable.from([]);
    });
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SkillAdderComponent, StarBarComponent],
      imports: [MaterialModule.forRoot()],
      providers: [
        { provide: SkillService, useValue: skillServiceMock }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillAdderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a name input', () => {
    expect(fixture.debugElement.query(By.css('#skill-input'))).toBeDefined();
  });

  it('should have 5 disabled stars', () => {
    const stars = fixture.debugElement.queryAll(By.css('button[disabled] .icon'));
    expect(stars.length).toBe(5);
  });

  it('should have an add button', () => {
    const button = fixture.debugElement.query(By.css('button#add'));
    expect(button).toBeDefined();
    expect(button.properties['disabled']).toBeTruthy();
    expect(button.nativeElement.textContent).toContain('Add Skill');
  });

  describe('when adding a name into name field', () => {
    beforeEach(() => {
      component.skillInput.value = 'Javascript';
      fixture.detectChanges();
    });

    it('should enable stars', () => {
      const stars = fixture.debugElement.queryAll(By.css('button.rating'));
      expect(stars.length).toBe(5);
      expect(stars.some(x => x.properties['disabled'])).toBeFalsy();
    });

    it('should enable the add button', () => {
      const button = fixture.debugElement.query(By.css('button#add'));
      expect(button).toBeDefined();
      expect(button.properties['disabled']).toBeFalsy();
    });

    describe('when clicking the third star', () => {
      beforeEach(() => {
        fixture.debugElement.queryAll(By.css('button.rating'))[2].triggerEventHandler('click', null);
      });

      it('should mark the star and all before it as set', () => {
        expect(component.ratings[0].isSet);
        expect(component.ratings[1].isSet);
        expect(component.ratings[2].isSet);
        expect(component.ratings[3].isSet).toBeFalsy();
        expect(component.ratings[4].isSet).toBeFalsy();
      });

      describe('when clicking add button', () => {
        beforeEach(() => {
          const button = fixture.debugElement.query(By.css('button#add')).triggerEventHandler('click', null);
        });

        it('should pass name and rating to service', () => {
          expect(skillServiceMock.add).toHaveBeenCalledWith('Javascript', 3);
        });

        it('should reset component', () => {
          expect(component.skillInput.value).toBe('');
          expect(component.ratings.some(x => x.isSet)).toBeFalsy();
        });
      });
    });
  });
});
