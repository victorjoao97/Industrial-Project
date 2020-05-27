using EnergyAndMaterialBalanceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class BGroupsRepository : GenericRepository<Bgroups, SEICBalanceContext>, IBGroupsRepository
    {
        public BGroupsRepository(SEICBalanceContext context) : base(context)
        {

        }

        public IEnumerable<Bgroups> GetAllBgroups(int resourceId)
        {
            return Context.Bgroups.Where(t => t.ResourceId == resourceId).AsEnumerable();
        }

        public IEnumerable<Bgroups> GetRootBGroups(int resourceId)
        {
            return Context.Bgroups.Where(t => t.ResourceId == resourceId && t.BgroupIdparent == null).AsEnumerable();
        }
    }
}
