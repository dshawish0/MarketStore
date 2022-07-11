using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProudctCustomer
    {
        public decimal PcId { get; set; }
        public decimal? ProudctId { get; set; }
        public decimal? CustomerId { get; set; }
        public decimal? Quaninty { get; set; }
        public DateTime? DateForm { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Proudct Proudct { get; set; }
    }
}
