using Cinode.Skills.Api.Repositories;
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
    }
}
