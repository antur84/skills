using Cinode.Skills.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinode.Api.Tests.Handlers
{
    public interface ISkillsHandler
    {
        Task<IEnumerable<SkillViewModel>> GetAllSkills();
    }
}