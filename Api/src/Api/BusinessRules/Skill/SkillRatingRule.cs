namespace Cinode.Skills.Api.BusinessRules.Skill
{
    public class SkillRatingRule
    {
        public static int Enforce(int rating)
        {
            if (rating < 0 || rating > 5)
            {
                throw new BusinessRuleException("Rating must be between 0 and 5");
            }

            return rating;
        }
    }
}
