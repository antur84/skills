import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-skill-deleter',
  templateUrl: './skill-deleter.component.html',
  styleUrls: ['./skill-deleter.component.css']
})
export class SkillDeleterComponent implements OnInit {

  @Output() onDeleteSkill: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  ngOnInit() {
  }

  onDelete() {
    this.onDeleteSkill.emit();
  }
}
