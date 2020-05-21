using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Bgroups1
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public short ResourceId { get; set; }
        public string Header { get; set; }
        public float? ValidDisbalance { get; set; }
    }
}
