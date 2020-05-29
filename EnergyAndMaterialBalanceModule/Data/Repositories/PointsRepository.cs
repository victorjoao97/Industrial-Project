using System;
using System.Collections.Generic;
using System.Linq;
using EnergyAndMaterialBalanceModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public class PointsRepository : BaseRepository<Points, SEICBalanceContext>, IPointsRepository
    {
        public PointsRepository(SEICBalanceContext context) : base(context)
        {
        }

        public IEnumerable<Points> GetAllPoints(int bgroupId)
        {
            return Context.Points.Where(t => t.BgroupId == bgroupId).Include(t => t.Period).Include(t => t.Source).AsEnumerable();
        }
    }
}
