using Cinode.Skills.Api.BusinessRules;
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
        private int id = 0;
        private ConcurrentBag<Skill> skills = new ConcurrentBag<Skill>();

        public void Add(Skill dbEntityToAdd)
        {
            if (skills.Any(x => x.Name == dbEntityToAdd.Name))
            {
                throw new BusinessRuleException("Skill already exists, " + dbEntityToAdd.Name);
            }
            id++;
            dbEntityToAdd.Id = id;
            skills.Add(dbEntityToAdd);
        }

        public Task<IEnumerable<Skill>> GetAll()
        {
            return Task.FromResult(skills.AsEnumerable());
        }
    }
}
