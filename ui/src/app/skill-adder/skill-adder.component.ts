import { Rating } from './rating';
import { SkillService } from './../skill.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MdInput } from '@angular/material';

@Component({
  selector: 'app-skill-adder',
  templateUrl: './skill-adder.component.html',
  styleUrls: ['./skill-adder.component.css']
})
export class SkillAdderComponent implements OnInit {

  @ViewChild('skillInput') skillInput: MdInput;

  ratings: Rating[];

  constructor(private skillService: SkillService) {
  }

  ngOnInit() {
    this.reset();
  }

  addSkill() {
    this.skillService.add(this.skillInput.value, this.getSelectedRating().id).subscribe(x => {
    }, (err) => {
      console.log(err);
    });
    this.reset();
  }

  onHoverRating(rating: Rating) {
    this.ratings.forEach(x => {
      x.isHovered = x.id <= rating.id;
    });
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
  }

  isAddSkillButtonDisabled() {
    return this.isSkillNameEmpty();
  }

  isRatingButtonDisabled() {
    return this.isSkillNameEmpty();
  }

  onHoverExit() {
    this.ratings.forEach(x => x.isHovered = false);
  }

  getRatingColor(rating: Rating) {
    const isHovering = this.ratings.some(x => x.isHovered);
    if (isHovering) {
      return rating.isHovered ? 'accent' : 'primary';
    }
    return rating.isSet ? 'accent' : 'primary';
  }

  private getSelectedRating() {
    return this.ratings.reduce((prev, curr) => {
      return curr.isSet ? curr : prev;
    }, new Rating());
  }

  private reset() {
    this.ratings = [];
    for (let i = 1; i <= 5; i++) {
      this.ratings.push({
        isSet: false,
        isHovered: false,
        id: i
      });
    }
    this.skillInput.focus();
    this.skillInput.value = '';
  }

  private isSkillNameEmpty() {
    return !this.skillInput.value;
  }
}
