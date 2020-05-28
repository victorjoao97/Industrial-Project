using EnergyAndMaterialBalanceModule.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class BGroupsRepository : BaseRepository<Bgroups, SEICBalanceContext>, IBGroupsRepository
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

        public IEnumerable<Bgroups> GetChildren(int groupid)
        {
            return Context.Bgroups.Where(t => t.BgroupIdparent.HasValue && t.BgroupIdparent.Value == groupid).AsEnumerable();
        }
    }
}
