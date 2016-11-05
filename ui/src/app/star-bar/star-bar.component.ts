import { Rating } from './../skill-adder/rating';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-star-bar',
  templateUrl: './star-bar.component.html',
  styleUrls: ['./star-bar.component.css']
})
export class StarBarComponent implements OnInit {

  @Input() ratings: Rating[];

  @Input() disabled: boolean;

  @Output() ratingChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

  onHoverExit() {
    this.ratings.forEach(x => x.isHovered = false);
  }

  isRatingButtonDisabled() {
    return this.disabled;
  }

  onClickRating(rating: Rating) {
    const selected = this.getSelectedRating();
    let newValue = (x: Rating) => x.id <= rating.id;
    if (selected === rating && selected.isSet) {
      newValue = () => false;
    }

    this.ratings.forEach(x => {
      x.isSet = newValue(x);
    });

    this.ratingChanged.emit(this.getSelectedRating().id);
  }


  getRatingColor(rating: Rating) {
    const isHovering = this.ratings.some(x => x.isHovered);
    if (isHovering) {
      return rating.isHovered ? 'accent' : 'primary';
    }
    return rating.isSet ? 'accent' : 'primary';
  }

  onHoverRating(rating: Rating) {
    this.ratings.forEach(x => {
      x.isHovered = x.id <= rating.id;
    });
  }

  private getSelectedRating() {
    return this.ratings.reduce((prev, curr) => {
      return curr.isSet ? curr : prev;
    }, new Rating());
  }
}
