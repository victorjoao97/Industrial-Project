using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class Points
    {
        public Points()
        {
            Rules = new HashSet<Rules>();
        }

        public int PointId { get; set; }
        public int BgroupId { get; set; }
        public string Direction { get; set; }
        public string Tagname { get; set; }
        public int? PeriodId { get; set; }
        public float? ValidMistake { get; set; }
        public int? SourceId { get; set; }
        public string PointName { get; set; }

        public virtual Bgroups Bgroup { get; set; }
        public virtual Periods Period { get; set; }
        public virtual Sources Source { get; set; }
        public virtual ICollection<Rules> Rules { get; set; }
    }
}
