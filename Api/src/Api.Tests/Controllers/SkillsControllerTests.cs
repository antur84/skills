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
            sut = new SkillsController(skillsHandlerMock.Object);
        }

        [Test]
        public async Task Get_should_return_from_handler()
        {
            var actualSkills = await sut.Get();
            Assert.That(actualSkills, Is.EqualTo(expectedSkills));
        }
    }
}
