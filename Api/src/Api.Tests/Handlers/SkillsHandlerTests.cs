using Cinode.Skills.Api.Handlers;
using Cinode.Skills.Api.Mappers;
using Cinode.Skills.Api.Models;
using Cinode.Skills.Api.Repositories;
using Cinode.Skills.Api.Repositories.DataModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Tests.Handlers
{
    [TestFixture]
    public class SkillsHandlerTests
    {
        private SkillsHandler sut;
        private Mock<IRepository<Skill>> skillsRepositoryMock;
        private Mock<IMapper<Skill, SkillViewModel>> mapperMock;
        private IEnumerable<Skill> dbSkills;

        [SetUp]
        public void Setup()
        {
            dbSkills = new List<Skill>
            {
                new Skill(),
                new Skill(),
                new Skill()
            };
            skillsRepositoryMock = new Mock<IRepository<Skill>>();
            skillsRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(dbSkills);
            skillsRepositoryMock.Setup(x => x.Add(It.IsAny<Skill>()));

            mapperMock = new Mock<IMapper<Skill, SkillViewModel>>();
            mapperMock.Setup(x => x.Map(It.IsAny<Skill>())).Returns(() => new SkillViewModel(Guid.Empty, "", 0));

            sut = new SkillsHandler(skillsRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public async Task GetAllSkills_should_return_mapped_skills_from_repo()
        {
            var actual = await sut.GetAllSkills();
            Assert.That(actual.Count(), Is.EqualTo(dbSkills.Count()));
        }

        [Test]
        public void Add_should_add_to_repo()
        {
            var dbSkill = new Skill();
            var skill = new SkillViewModel(Guid.Parse("8751a649-be2f-4a16-b310-cbc3d9467598"), "Javascript", 5);
            mapperMock.Setup(x => x.MapReverse(skill)).Returns(dbSkill);

            sut.Add(skill);

            skillsRepositoryMock.Verify(x => x.Add(dbSkill), Times.Once);
        }
    }
}
