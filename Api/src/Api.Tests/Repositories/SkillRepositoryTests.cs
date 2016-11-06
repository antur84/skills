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
        public async Task GetAll_should_return_with_newest_first()
        {
            sut.Add(new Skill
            {
                Name = "Hej"
            });
            System.Threading.Thread.Sleep(1);
            sut.Add(new Skill
            {
                Name = "Hej2"
            });
            var list = await sut.GetAll();
            Assert.That(list.First().Name, Is.EqualTo("Hej2"));
            Assert.That(list.Count(), Is.EqualTo(2));
            Assert.That(list.Last().Name, Is.EqualTo("Hej"));
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
            Assert.That(actual.Created, Is.Not.Null);
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

        [Test]
        public void Update_should_throw_when_not_found()
        {
            var skill = new Skill
            {
                Name = "Hej"
            };
            Assert.Throws<BusinessRuleException>(() => sut.Update(skill));
        }

        [Test]
        public async Task Update_should_update_name_and_rating_of_existing()
        {
            var guid = Guid.Parse("8504c134-3b53-449d-8a83-5809733c7f52");
            sut.Add(new Skill
            {
                Name = "Hej",
                RatingPercentage = 49,
                ExternalId = guid
            });

            var updatedSkill = new Skill
            {
                Name = "Hej2",
                RatingPercentage = 100,
                ExternalId = guid
            };
            sut.Update(updatedSkill);

            var all = await sut.GetAll();
            Assert.That(all.Single() != null, Is.True);
            Assert.That(all.Single().Name, Is.EqualTo(updatedSkill.Name));
            Assert.That(all.Single().RatingPercentage, Is.EqualTo(updatedSkill.RatingPercentage));
        }

        [Test]
        public void Delete_should_throw_when_not_found()
        {
            Assert.Throws<BusinessRuleException>(() => sut.Delete(Guid.Parse("8504c134-3b53-449d-8a83-5809733c7f52")));
        }

        [Test]
        public async Task Delete_should_remove_existing()
        {
            var guid = Guid.Parse("8504c134-3b53-449d-8a83-5809733c7f52");
            sut.Add(new Skill
            {
                Name = "Hej",
                RatingPercentage = 49,
                ExternalId = guid
            });

            sut.Delete(guid);

            var all = await sut.GetAll();
            Assert.That(all, Is.Empty);
        }
    }
}
