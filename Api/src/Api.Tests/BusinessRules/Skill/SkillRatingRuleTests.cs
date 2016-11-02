using Cinode.Skills.Api.BusinessRules;
using Cinode.Skills.Api.BusinessRules.Skill;
using Cinode.Skills.Api.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Tests
{
    [TestFixture]
    public class SkillRatingRuleTests
    {
        [Test]
        public void Enforce_should_throw_on_negative([Values(-1, -100, int.MinValue)] int value)
        {
            Assert.Throws<BusinessRuleException>(() => SkillRatingRule.Enforce(value));
        }

        [Test]
        public void Enforce_should_return_input_when_valid([Values(0, 1, 2, 3, 4, 5)] int value)
        {
            Assert.That(SkillRatingRule.Enforce(value), Is.EqualTo(value));
        }

        [Test]
        public void Enforce_should_throw_on_greater_than_5([Values(6, 100, int.MaxValue)] int value)
        {
            Assert.Throws<BusinessRuleException>(() => SkillRatingRule.Enforce(value));
        }
    }
}
