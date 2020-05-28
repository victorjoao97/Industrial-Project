﻿using EnergyAndMaterialBalanceModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyAndMaterialBalanceModule.Data.Repositories
{
    public interface IBGroupsRepository : IBaseRepository<Bgroups>
    {
        IEnumerable<Bgroups> GetAllBgroups(int resourceId);
        IEnumerable<Bgroups> GetRootBGroups(int resourceId);
        Task<IEnumerable<Bgroups>> GetChildrenAsync(int groupid);
        Task<Bgroups> GetAllChildren(int groupid);
    }
}
