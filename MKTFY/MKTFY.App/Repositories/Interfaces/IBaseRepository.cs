using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    // Any reference to this base class must call for all the listed TASK under the Interface
    public interface IBaseRepository<TEntity, TPrimaryKey>
    {
        Task<TEntity> Create(TEntity src);
        Task<TEntity> Get(TPrimaryKey id);
        Task<List<TEntity>> GetAll();
        Task Delete(TPrimaryKey id);
    }
}
