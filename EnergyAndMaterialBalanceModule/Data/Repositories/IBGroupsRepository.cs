using EnergyAndMaterialBalanceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IBGroupsRepository
    {
        Task<IEnumerable<Bgroups>> GetAllBgroups();
        Task<IEnumerable<Bgroups>> GetRootBgroups(int resourceId);
        IEnumerable<Bgroups> GetChildren(int parentId);
        Bgroups GetById(int id);
    }
}
