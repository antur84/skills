/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StarBarComponent } from './star-bar.component';
import { MaterialModule, MdInput, MdButton } from '@angular/material';

describe('StarBarComponent', () => {
  let component: StarBarComponent;
  let fixture: ComponentFixture<StarBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [StarBarComponent],
      imports: [MaterialModule.forRoot()],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StarBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
