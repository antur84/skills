import { SkillViewModelWithStars } from './skillViewModelWithStars';
import { Rating } from './../skill-adder/rating';
import { SkillService } from './../skill.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-skill-lister',
  templateUrl: './skill-lister.component.html',
  styleUrls: ['./skill-lister.component.css']
})
export class SkillListerComponent implements OnInit {

  skills: SkillViewModelWithStars[];
  constructor(private skillsService: SkillService) { }

  ngOnInit() {
    this.skillsService.getAll().subscribe(skills => {
      this.skills = skills.map(skill => {
        return {
          externalId: skill.externalId,
          name: skill.name,
          ratings: this.createRatings(skill.rating)
        };
      });
    });
  }

  private createRatings(rating: number): Rating[] {
    let result: Rating[] = [];
    for (let i = 1; i <= 5; i++) {
      result.push({
        id: i,
        isSet: i <= rating,
        isHovered: false
      });
    }
    return result;
  }
}
