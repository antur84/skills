using System;
using System.Collections.Generic;
using Cinode.Api.Tests.Handlers;
using Cinode.Skills.Api.Models;
using System.Threading.Tasks;
using Cinode.Skills.Api.Repositories;
using Cinode.Skills.Api.Mappers;
using System.Linq;
using Cinode.Skills.Api.Repositories.DataModels;

namespace Cinode.Skills.Api.Handlers
{
    public class SkillsHandler : ISkillsHandler
    {
        private readonly IRepository<Skill> repository;
        private readonly IMapper<Skill, SkillViewModel> mapper;

        public SkillsHandler(IRepository<Skill> repository, IMapper<Skill, SkillViewModel> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SkillViewModel>> GetAllSkills()
        {
            var dbSkills = await repository.GetAll();
            return dbSkills.Select(x => mapper.Map(x)); 
        }

        public void Add(SkillViewModel skill)
        {
            var dbEntityToAdd = mapper.MapReverse(skill);
            repository.Add(dbEntityToAdd);
        }
    }
}
