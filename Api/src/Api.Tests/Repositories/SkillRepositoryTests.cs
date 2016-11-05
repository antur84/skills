using Cinode.Skills.Api.BusinessRules;
using Cinode.Skills.Api.Repositories;
using Cinode.Skills.Api.Repositories.DataModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Tests.Repositories
{
    [TestFixture]
    public class SkillRepositoryTests
    {
        private SkillRepository sut;

        [SetUp]
        public void Setup()
        {
            sut = new SkillRepository();
        }

        [Test]
        public async Task GetAll_should_return_empty_list_when_nothing_added()
        {
            Assert.That(await sut.GetAll(), Is.Empty);
        }

        [Test]
        public async Task Add_should_add()
        {
            sut.Add(new Skill
            {
                Name = "Hej"
            });

            var actual = (await sut.GetAll()).Single();
            Assert.That(actual.Name, Is.EqualTo("Hej"));
            Assert.That(actual.Id, Is.EqualTo(1));
        }

        [Test]
        public void Add_should_throw_on_duplicate_names()
        {
            var skill = new Skill
            {
                Name = "Hej"
            };
            sut.Add(skill);

            Assert.Throws<BusinessRuleException>(() => sut.Add(skill));
        }
    }
}
