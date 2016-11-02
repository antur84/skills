using Cinode.Skills.Api.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Tests
{
    [TestFixture]
    public class SkillViewModelTests
    {
        private SkillViewModel sutA;
        private SkillViewModel sutB;

        [SetUp]
        public void Setup()
        {
            sutA = new SkillViewModel(Guid.Parse("d403af6d-946e-4856-9706-8f06dfc30d1c"), "Andreas", 0);
            sutB = new SkillViewModel(Guid.Parse("d403af6d-946e-4856-9706-8f06dfc30d1c"), "Andreas", 0);
        }

        [Test]
        public void GetHashCode_should_return_same_for_same_ExternalId()
        {
            Assert.That(sutA.GetHashCode(), Is.EqualTo(sutB.GetHashCode()));
        }

        [Test]
        public void Equals_should_return_true_when_ExternalId_matches()
        {
            Assert.That(sutA.Equals(sutB), Is.True);
        }

        [Test]
        public void Equals_should_return_false_when_ExternalId_differs()
        {
            sutA = new SkillViewModel(Guid.Parse("f7bcaff1-6027-4e30-b1c4-350cffa9224e"), "Andreas", 0);
            Assert.That(sutA.Equals(sutB), Is.False);
        }
    }
}
