using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class PrulesRepository : BaseRepository<Prule, SEICBalanceContext>, IPruleRepository
    {
        public PrulesRepository(SEICBalanceContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Prule>> GetAllRules()
        {
            return (IEnumerable<Prule>)await GetAll().ToListAsync();
        }

    }
}
