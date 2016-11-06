using Cinode.Skills.Api.BusinessRules.Skill;
using System;

namespace Cinode.Skills.Api.Models
{
    public class SkillViewModel
    {
        public SkillViewModela(Guid externalId, string name, int rating)
        {
            Name = SkillNameRule.Enforce(name);
            Rating = SkillRatingRule.Enforce(rating);
            ExternalId = externalId;
        }

        public Guid ExternalId { get; }

        public string Name { get; }

        public int Rating { get;}

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var objSkill = obj as SkillViewModel;
            if (objSkill == null)
            {
                return false;
            }

            return (ExternalId == objSkill.ExternalId);
        }

        public override int GetHashCode()
        {
            var hash = 42;
            hash = hash * 31 + ExternalId.GetHashCode();
            return hash;
        }
    }
}
