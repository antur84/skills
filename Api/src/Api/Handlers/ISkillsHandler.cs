using Cinode.Skills.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Cinode.Api.Tests.Handlers
{
    public interface ISkillsHandler
    {
        Task<IEnumerable<SkillViewModel>> GetAllSkills();
        void Add(SkillViewModel skill);
        void Update(SkillViewModel model);
        void Delete(Guid externalId);
    }
}