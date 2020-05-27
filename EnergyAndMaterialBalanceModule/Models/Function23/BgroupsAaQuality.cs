using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class BgroupsAaQuality
    {
        public int QualityTableIdentity { get; set; }
        public string ColumnName { get; set; }
        public DateTime Timestamp { get; set; }
        public short Quality { get; set; }

        public virtual Bgroups QualityTableIdentityNavigation { get; set; }
    }
}
