using Cinode.Skills.Api.Models;
using Cinode.Skills.Api.Repositories.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Skills.Api.Mappers
{
    public class SkillViewModelMapper : IMapper<Skill, SkillViewModel>
    {
        private const int SkillLevels = 5;

        public SkillViewModel Map(Skill from)
        {
            return new SkillViewModel(from.ExternalId, from.Name, ConvertRatingPercentageToNumber(from.RatingPercentage));
        }

        private static int ConvertRatingPercentageToNumber(int number)
        {
            var v = (decimal)number / 100 * SkillLevels;
            return (int)Math.Round(v);
        }
    }
}
