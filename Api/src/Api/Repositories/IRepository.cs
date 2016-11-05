using System.Collections.Generic;
using System.Threading.Tasks;
using Cinode.Skills.Api.Repositories.DataModels;

namespace Cinode.Skills.Api.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        void Add(Skill dbEntityToAdd);
    }
}