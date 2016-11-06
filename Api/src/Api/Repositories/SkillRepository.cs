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
        private ConcurrentDictionary<int, Skill> skills = new ConcurrentDictionary<int, Skill>();

        public void Add(Skill dbEntityToAdd)
        {
            if (skills.Any(x => x.Value.Name == dbEntityToAdd.Name))
            {
                throw new BusinessRuleException("Skill already exists, " + dbEntityToAdd.Name);
            }
            id++;
            dbEntityToAdd.Id = id;
            dbEntityToAdd.Created = DateTime.UtcNow;
            skills[id] = dbEntityToAdd;
        }

        public void Delete(Guid externalId)
        {
            Skill removed;
            var id = GetOrThrow(externalId).Id;
            skills.TryRemove(id, out removed);
        }

        public Task<IEnumerable<Skill>> GetAll()
        {
            var orderedSkills = skills.Values.OrderByDescending(x => x.Created);
            return Task.FromResult(orderedSkills.AsEnumerable());
        }

        public void Update(Skill dbEntityToUpdate)
        {
            Skill current = GetOrThrow(dbEntityToUpdate.ExternalId);

            current.Name = dbEntityToUpdate.Name;
            current.RatingPercentage = dbEntityToUpdate.RatingPercentage;
        }

        private Skill GetOrThrow(Guid externalId)
        {
            var current = skills.Values.FirstOrDefault(x => x.ExternalId == externalId);
            if (current == null)
            {
                throw new BusinessRuleException("Skill does not exist");
            }

            return current;
        }
    }
}
