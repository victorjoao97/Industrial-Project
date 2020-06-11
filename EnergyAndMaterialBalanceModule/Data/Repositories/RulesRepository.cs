using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class RulesRepository : BaseRepository<Rules, SEICBalanceContext>, IRulesRepository
    {
        public RulesRepository(SEICBalanceContext context) : base(context)
        {

        }

        async Task<IEnumerable<Rules>> IRulesRepository.GetAllRules()
        {
            return (IEnumerable<Rules>)await GetAll().ToListAsync();
        }

        async Task<IEnumerable<Rules>> IRulesRepository.GetAllFormulas()
        {
            return (IEnumerable<Rules>)await GetAll().ToListAsync();
        }
    }
}