namespace Cinode.Skills.Api.BusinessRules.Skill
{
    public class SkillNameRule
    {
        public static string Enforce(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessRuleException("Skill must have a name");
            }

            return name;
        }
    }
}
