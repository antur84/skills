using Cinode.Skills.Api.Mappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinode.Skills.Api.Repositories.DataModels;

namespace Cinode.Api.Tests.Mappers
{
    [TestFixture]
    public class SkillViewModelMapperTests
    {
        private SkillViewModelMapper sut;
        private Skill skill;

        [SetUp]
        public void Setup()
        {
            skill = new Skill
            {
                Name = "Test",
                ExternalId = Guid.Parse("7dda403f-ada0-4e90-bc9d-a42fefa11320"),
                Id = 1,
                RatingPercentage = 0
            };
            sut = new SkillViewModelMapper();
        }

        [Test]
        public void Map_should_convert_even_values_percent_to_fifths([Values(0, 20, 40, 60, 80, 100)] decimal percent)
        {
            skill.RatingPercentage = (int)percent;
            Assert.That(sut.Map(skill).Rating, Is.EqualTo(percent / 100 * 5));
        }

        [Test]
        public void Map_should_convert_odd_percent_to_closest_fifth()
        {
            skill.RatingPercentage = 11;
            Assert.That(sut.Map(skill).Rating, Is.EqualTo(1));
        }
    }
}
