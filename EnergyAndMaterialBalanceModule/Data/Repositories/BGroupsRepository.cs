using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class BGroupsRepository : BaseRepository<Bgroups, SEICBalanceContext>, IBGroupsRepository
    {
        public BGroupsRepository(SEICBalanceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Bgroups>> GetAllBgroups()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Bgroups>> GetRootBgroups(int resourceId)
        {
            return await Context.Set<Bgroups>().Where(b => b.ResourceId == resourceId && b.BgroupIdparent == null).Include(f => f.InverseBgroupIdparentNavigation).ToListAsync();
        }

        public IEnumerable<Bgroups> GetChildren(int parentId)
        {
            return Context.Set<Bgroups>().Where(f => (f.BgroupIdparent == parentId)).ToList();
        }

        public Bgroups GetById(int id)
        {
            return Context.Set<Bgroups>().Include(f => f.InverseBgroupIdparentNavigation).Where(b => b.BgroupId == id).First();
        }

    }
}
