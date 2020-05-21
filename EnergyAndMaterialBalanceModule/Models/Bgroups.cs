using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Bgroups
    {
        public Bgroups()
        {
            BgroupsAaQuality = new HashSet<BgroupsAaQuality>();
            InverseBgroupIdparentNavigation = new HashSet<Bgroups>();
            Points = new HashSet<Points>();
        }

        public int BgroupId { get; set; }
        public int? BgroupIdparent { get; set; }
        public short ResourceId { get; set; }
        public string BgroupName { get; set; }
        public float? ValidDisbalance { get; set; }

        public virtual Bgroups BgroupIdparentNavigation { get; set; }
        public virtual Resources Resource { get; set; }
        public virtual ICollection<BgroupsAaQuality> BgroupsAaQuality { get; set; }
        public virtual ICollection<Bgroups> InverseBgroupIdparentNavigation { get; set; }
        public virtual ICollection<Points> Points { get; set; }
    }
}
