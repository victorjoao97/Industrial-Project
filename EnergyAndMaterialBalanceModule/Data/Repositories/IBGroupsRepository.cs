using EnergyAndMaterialBalanceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IBGroupsRepository
    {
        IEnumerable<Bgroups> GetAllBgroups(int resourceId);
        IEnumerable<Bgroups> GetRootBGroups(int resourceId);
        IEnumerable<Bgroups> GetChildren(int groupid);
    }
}
