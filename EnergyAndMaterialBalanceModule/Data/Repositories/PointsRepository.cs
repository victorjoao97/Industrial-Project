using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class PointsRepository : BaseRepository<Points, SEICBalanceContext>, IPointsRepository
    {
        public PointsRepository(SEICBalanceContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Points>> GetAlPonts(int bgroupId)
        {
            return await Context.Points.Where(t => t.BgroupId == bgroupId).Include(f => f.Source).Include(f => f.Period).ToListAsync();
        }

    }
}
