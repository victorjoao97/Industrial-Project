using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class ResourcesRepository : BaseRepository<Resources, SEICBalanceContext>, IResourcesRepository
    {
        public ResourcesRepository(SEICBalanceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Resources>> GetAllResources()
        {
            return await GetAll().ToListAsync();

        }


    }
}