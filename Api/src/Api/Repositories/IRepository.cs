using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinode.Skills.Api.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}