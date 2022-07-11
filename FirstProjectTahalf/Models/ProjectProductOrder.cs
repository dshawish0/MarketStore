using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectProductOrder
    {
        public decimal Productorderid { get; set; }
        public decimal Proid { get; set; }
        public decimal Orderid { get; set; }

        public virtual ProjectOrder Order { get; set; }
        public virtual ProjectProduct Pro { get; set; }
    }
}
