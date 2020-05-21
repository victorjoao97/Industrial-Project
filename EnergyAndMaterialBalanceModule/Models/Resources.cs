using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Resources
    {
        public Resources()
        {
            Bgroups = new HashSet<Bgroups>();
        }

        public short ResourceId { get; set; }
        public string ResourceName { get; set; }

        public virtual ICollection<Bgroups> Bgroups { get; set; }
    }
}
