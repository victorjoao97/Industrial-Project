using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class BGroupsRepository : BaseRepository<Bgroups, SEICBalanceContext>, IBGroupsRepository
    {
        public BGroupsRepository(SEICBalanceContext context) : base(context)
        {

        }

        public virtual async Task<Bgroups> GetById(int id)
        {
            return await Context.Set<Bgroups>().Where(t=> t.BgroupId == id).Include(t=> t.InverseBgroupIdparentNavigation).Include(t => t.Points).ThenInclude(t => t.Rules).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Bgroups>> GetAllBgroups(int resourceId)
        {
            return await Context.Bgroups.Where(t => t.ResourceId == resourceId).Include(t => t.InverseBgroupIdparentNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Bgroups>> GetRootBGroups(int resourceId)
        {
            return await Context.Bgroups.Where(t => t.ResourceId == resourceId && t.BgroupIdparent == null).Include(t => t.InverseBgroupIdparentNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Bgroups>> GetChildrenAsync(int groupid)
        {
            return await Context.Bgroups.Where(t => t.BgroupIdparent.HasValue && t.BgroupIdparent.Value == groupid).Include(t => t.InverseBgroupIdparentNavigation).ToArrayAsync();
        }

        public async Task<Bgroups> GetAllChildren(int groupid)
        {
            var g = await GetById(groupid);
            await LoadChildren(g);
            return g;
        }

        private async Task LoadChildren(Bgroups group)
        {
            if (group.InverseBgroupIdparentNavigation != null)
            {
                foreach (var g in group.InverseBgroupIdparentNavigation)
                {
                    var children = await GetChildrenAsync(g.BgroupId);
                    g.InverseBgroupIdparentNavigation = children.ToList();
                    await LoadChildren(g);
                }
            }
        }
    }
}
