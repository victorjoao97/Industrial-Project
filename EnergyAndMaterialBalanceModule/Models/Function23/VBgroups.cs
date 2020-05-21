using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class VBgroups
    {
        public int BgroupId { get; set; }
        public int? BgroupIdparent { get; set; }
        public short ResourceId { get; set; }
        public string BgroupName { get; set; }
        public float? ValidDisbalance { get; set; }
    }
}
