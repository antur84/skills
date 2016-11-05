using Cinode.Skills.Api.Repositories.DataModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Skills.Api.Repositories
{
    public class SkillRepository : IRepository<Skill>
    {
        private ConcurrentBag<Skill> skills = new ConcurrentBag<Skill>();

        public void Add(Skill dbEntityToAdd)
        {
            this.skills.Add(dbEntityToAdd);
        }

        public Task<IEnumerable<Skill>> GetAll()
        {
            return Task.FromResult(skills.AsEnumerable());
        }
    }
}
