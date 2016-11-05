import { Injectable } from '@angular/core';

@Injectable()
export class SkillService {

  constructor() { }

  add(name: string, rating: number) {
    console.log('click', name, rating);
  }
}
