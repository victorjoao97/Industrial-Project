using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Sources
    {
        public Sources()
        {
            Points = new HashSet<Points>();
        }

        public int SourceId { get; set; }
        public string SourceName { get; set; }

        public virtual ICollection<Points> Points { get; set; }
    }
}
