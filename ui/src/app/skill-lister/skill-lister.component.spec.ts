/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SkillListerComponent } from './skill-lister.component';

describe('SkillListerComponent', () => {
  let component: SkillListerComponent;
  let fixture: ComponentFixture<SkillListerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SkillListerComponent ]
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
});
