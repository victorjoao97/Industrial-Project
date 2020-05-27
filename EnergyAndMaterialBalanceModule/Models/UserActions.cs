using System;
using System.Collections.Generic;

namespace EnergyAndMaterialBalanceModule.Models
{
    public partial class UserActions
    {
        public DateTime? Dt { get; set; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public string Comment { get; set; }
    }
}
