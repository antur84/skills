import { Rating } from './../skill-adder/rating';
/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StarBarComponent } from './star-bar.component';
import { MaterialModule, MdInput, MdButton } from '@angular/material';

describe('StarBarComponent', () => {
  let component: StarBarComponent;
  let fixture: ComponentFixture<StarBarComponent>;
  let rating: Rating;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [StarBarComponent],
      imports: [MaterialModule.forRoot()],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    rating = new Rating();
    rating.id = 1;
    const rating2 = Object.assign({}, rating);
    rating2.id = 2;
    fixture = TestBed.createComponent(StarBarComponent);
    component = fixture.componentInstance;
    component.ratings = [rating, rating2];
    component.disabled = false;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('onClickRating', () => {
    let changed = -1;

    beforeEach(() => {
      component.ratingChanged.subscribe((x) => {
        changed = x;
      });
      component.onClickRating(rating);
    });

    it('emits the top selected value', () => {
      expect(changed).toBe(rating.id);
    });
  });
});
