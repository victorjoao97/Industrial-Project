using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyAndMaterialBalanceModule.Models;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IPointsRepository : IBaseRepository<Points>
    {
        Task<IEnumerable<Points>> GetAlPonts(int bgroupId);
    }
}