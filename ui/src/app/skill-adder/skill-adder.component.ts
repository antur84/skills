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

  private changedRating: number;

  ratings: Rating[];

  @ViewChild('skillInput') skillInput: MdInput;

  constructor(private skillService: SkillService) {
  }

  ngOnInit() {
    this.reset();
  }

  addSkill() {
    this.skillService.add(this.skillInput.value, this.changedRating).subscribe(x => {
    }, (err) => {
      console.log(err);
    });
    this.reset();
  }

  isAddSkillButtonDisabled() {
    return this.isSkillNameEmpty();
  }

  isSkillNameEmpty() {
    return !this.skillInput.value;
  }

  onRatingChanged(rating: number) {
    this.changedRating = rating;
  }

  private reset() {
    this.skillInput.focus();
    this.skillInput.value = '';
    this.ratings = [];
    for (let i = 1; i <= 5; i++) {
      this.ratings.push({
        isSet: false,
        isHovered: false,
        id: i
      });
    }
  }
}
