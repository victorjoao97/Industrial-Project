using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Periods
    {
        public Periods()
        {
            Points = new HashSet<Points>();
        }

        public int PeriodId { get; set; }
        public string PeriodName { get; set; }

        public virtual ICollection<Points> Points { get; set; }
    }
}
