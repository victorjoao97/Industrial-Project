using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class BalanceClosed
    {
        public int BgroupId { get; set; }
        public int? BgroupIdparent { get; set; }
        public string BgroupName { get; set; }
        public float? ValidDisbalance { get; set; }
        public string PointName { get; set; }
        public string Direction { get; set; }
        public int? SourceId { get; set; }
        public string TagName { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? TimeStampMs { get; set; }
        public float? Value { get; set; }
        public DateTime? Period { get; set; }
        public DateTime P { get; set; }
        public DateTime? DateClosed { get; set; }
        public string UserName { get; set; }
    }
}
