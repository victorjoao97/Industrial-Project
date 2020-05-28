using EnergyAndMaterialBalanceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IBGroupsRepository : IBaseRepository<Bgroups>
    {
        IEnumerable<Bgroups> GetAllBgroups(int resourceId);
        Task<IEnumerable<Bgroups>> GetRootBGroups(int resourceId);
        Bgroups GetBGroupsById(int bGroupsId);
        Task<IEnumerable<Bgroups>> GetChildrenAsync(int groupid);
        Task<Bgroups> GetAllChildren(int groupid);
    }
}
