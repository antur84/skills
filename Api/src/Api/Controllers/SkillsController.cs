using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinode.Skills.Api.Models;
using Cinode.Api.Tests.Handlers;

namespace Cinode.Skills.Api.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private ISkillsHandler skillsHandler;

        public SkillsController(ISkillsHandler skillsHandler)
        {
            this.skillsHandler = skillsHandler;
        }

        [HttpGet]
        public Task<IEnumerable<SkillViewModel>> Get()
        {
            return skillsHandler.GetAllSkills();
        }

        [HttpPost]
        public void Post([FromBody]SkillViewModel model)
        {
        }
    }
}
