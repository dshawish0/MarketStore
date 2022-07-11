using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectPayment
    {
        public decimal Paymentid { get; set; }
        public decimal Status { get; set; }
        public DateTime? Datee { get; set; }
        public decimal? Total { get; set; }
        public decimal Orderid { get; set; }

        public virtual ProjectOrder Order { get; set; }
    }
}
