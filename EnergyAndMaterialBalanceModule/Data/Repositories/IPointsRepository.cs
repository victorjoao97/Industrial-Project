using System.Collections.Generic;
using EnergyAndMaterialBalanceModule.Models;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IPointsRepository
    {
        IEnumerable<Points> GetAllPoints(int bgroupId);
    }
}