using Cinode.Api.Tests.Handlers;
using Cinode.Skills.Api.Controllers;
using Cinode.Skills.Api.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Tests.Controllers
{
    [TestFixture]
    public class SkillsControllerTests
    {
        private SkillsController sut;
        private Mock<ISkillsHandler> skillsHandlerMock;
        private IEnumerable<SkillViewModel> expectedSkills;

        [SetUp]
        public void Setup()
        {
            expectedSkills = new List<SkillViewModel>();
            skillsHandlerMock = new Mock<ISkillsHandler>();
            skillsHandlerMock.Setup(x => x.GetAllSkills()).ReturnsAsync(expectedSkills);
            skillsHandlerMock.Setup(x => x.Add(It.IsAny<SkillViewModel>()));
            sut = new SkillsController(skillsHandlerMock.Object);
        }

        [Test]
        public async Task Get_should_return_from_handler()
        {
            var actualSkills = await sut.Get();
            Assert.That(actualSkills.Data, Is.EqualTo(expectedSkills));
        }

        [Test]
        public void Post_should_pass_object_to_handler()
        {
            var skill = new SkillViewModel(Guid.Parse("a60a539d-e79c-43d9-a8dc-189c22f8387d"), "Javascript", 5);
            sut.Post(skill);
            skillsHandlerMock.Verify(x => x.Add(skill), Times.Once);
        }
    }
}
