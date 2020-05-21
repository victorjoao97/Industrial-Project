using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Prule
    {
        public int? RuleId { get; set; }
        public string TagName { get; set; }
        public string Param { get; set; }
        public int? SourceId { get; set; }

        public virtual Rules Rule { get; set; }
        public virtual Sources Source { get; set; }
    }
}
