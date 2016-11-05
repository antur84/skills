/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { MaterialModule, MdSnackBar } from '@angular/material';

import { SnackbarAnchorComponent } from './snackbar-anchor.component';

describe('SnackbarAnchorComponent', () => {
  let component: SnackbarAnchorComponent;
  let fixture: ComponentFixture<SnackbarAnchorComponent>;
  let snackbarMock: MdSnackBar;

  beforeEach(() => {
    snackbarMock = jasmine.createSpyObj('snackbar', ['open']);
  });

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SnackbarAnchorComponent],
      imports: [MaterialModule.forRoot()],
      providers: [
        { provide: MdSnackBar, useValue: snackbarMock }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SnackbarAnchorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
