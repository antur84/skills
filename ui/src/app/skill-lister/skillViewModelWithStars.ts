import { Rating } from './../skill-adder/rating';
export interface SkillViewModelWithStars {
    externalId: string;
    name: string;
    ratings: Rating[];
}
