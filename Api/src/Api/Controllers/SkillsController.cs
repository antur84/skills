using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinode.Skills.Api.Models;
using Cinode.Api.Tests.Handlers;
using Cinode.Api.Models;

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
        public async Task<ApiResponseViewModel<IEnumerable<SkillViewModel>>> Get()
        {
            var skills = await skillsHandler.GetAllSkills();
            return CreateOkResponse(skills);
        }

        [HttpPost]
        public ApiResponseViewModel<object> Post([FromBody]SkillViewModel model)
        {
            skillsHandler.Add(model);
            return CreateEmptyOkResponse();
        }

        [HttpPut]
        public ApiResponseViewModel<object> Put([FromBody]SkillViewModel model)
        {
            skillsHandler.Update(model);
            return CreateEmptyOkResponse();
        }

        [HttpDelete("{externalId}")]
        public ApiResponseViewModel<object> Delete(Guid externalId)
        {
            skillsHandler.Delete(externalId);
            return CreateEmptyOkResponse();
        }

        private ApiResponseViewModel<object> CreateEmptyOkResponse()
        {
            return CreateOkResponse<object>(null);
        }

        private ApiResponseViewModel<T> CreateOkResponse<T>(T data)
        {
            return new ApiResponseViewModel<T>
            {
                Code = 200,
                Message = null,
                Data = data
            };
        }
    }
}
