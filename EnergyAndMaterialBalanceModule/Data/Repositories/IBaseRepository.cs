using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity entity);
        Task Delete(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task Update(TEntity entity);
    }
}