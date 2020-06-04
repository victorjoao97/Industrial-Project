using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class BGroupsRepository : BaseRepository<Bgroups, SEICBalanceContext>, IBGroupsRepository
    {
        public BGroupsRepository(SEICBalanceContext context) : base(context)
        {

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

        public async Task<Bgroups> GetWithAllChildren(int groupid)
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

        public override Task<Bgroups> GetById(int id)
        {
            return Context.Bgroups.Where(t => t.BgroupId == id)
                .Include(t => t.Points)
                .ThenInclude(t => t.Source)
                .FirstAsync();
        }

        public async Task DeleteWithDependent(int groupid)
        {
            var g = await GetById(groupid);
            var visited = new Stack<Bgroups>();
            var result = new List<Bgroups>();
            visited.Push(g);
            while(visited.Count > 0)
            {
                var current = visited.Pop();
                foreach(var t in current.InverseBgroupIdparentNavigation)
                    visited.Push(t);
                result.Add(current);
            }

            foreach(var group in result)
            {
                Context.Bgroups.Remove(group);
            }
        }
    }
}
