import { SnackbarAnchorComponent } from './../snackbar-anchor/snackbar-anchor.component';
import { Rating } from './rating';
import { SkillService } from './../skill.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MdInput, MdSnackBar } from '@angular/material';

@Component({
  selector: 'app-skill-adder',
  templateUrl: './skill-adder.component.html',
  styleUrls: ['./skill-adder.component.css'],
  providers: [MdSnackBar]
})
export class SkillAdderComponent implements OnInit {

  private changedRating: number;

  ratings: Rating[];

  @ViewChild('skillInput') skillInput: MdInput;

  @ViewChild(SnackbarAnchorComponent) snackbarAnchor: SnackbarAnchorComponent;

  constructor(private skillService: SkillService, private snackbar: MdSnackBar) {
  }

  ngOnInit() {
    this.reset();
  }

  addSkill() {
    this.skillService.add(this.skillInput.value, this.changedRating).subscribe(x => {
    }, (err) => {
      this.snackbarAnchor.failedAttempt('Error : ' + err.code + ' : ' + err.message, 'Ok I get it');
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
    this.changedRating = 0;
    for (let i = 1; i <= 5; i++) {
      this.ratings.push({
        isSet: false,
        isHovered: false,
        id: i
      });
    }
  }
}
