/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SkillDeleterComponent } from './skill-deleter.component';
import { MaterialModule } from '@angular/material';

describe('SkillDeleterComponent', () => {
  let component: SkillDeleterComponent;
  let fixture: ComponentFixture<SkillDeleterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SkillDeleterComponent],
      imports: [MaterialModule.forRoot()]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillDeleterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('onDelete', () => {
    let spy;
    beforeEach(() => {
      spy = jasmine.createSpy('emit spy');
      component.onDeleteSkill.subscribe(spy);
      fixture.debugElement.query(By.css('button')).triggerEventHandler('click', null);
    });

    it('should emit', () => {
      expect(spy).toHaveBeenCalled();
    });
  });
});
