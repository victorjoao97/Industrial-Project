using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyAndMaterialBalanceModule.Models;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IResourcesRepository
    {
        Task<IEnumerable<Resources>> GetAllResources();
    }
}