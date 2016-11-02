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
    public class SkillNameRuleTests
    {
        [Test]
        public void Enforce_should_throw_on_null_blank_and_whitespace([Values(null, "", " ")] string value)
        {
            Assert.Throws<BusinessRuleException>(() => SkillNameRule.Enforce(value));
        }

        [Test]
        public void Enforce_should_return_input_when_valid([Values("AJAX", "C#", "Project Management")] string value)
        {
            Assert.That(SkillNameRule.Enforce(value), Is.EqualTo(value));
        }
    }
}
