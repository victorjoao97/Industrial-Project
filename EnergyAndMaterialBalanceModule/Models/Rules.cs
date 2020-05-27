using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Rules
    {
        public int RuleId { get; set; }
        public int? PointId { get; set; }
        public string Formula { get; set; }

        public virtual Points Point { get; set; }
    }
}
