using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class Proudct
    {
        public Proudct()
        {
            ProudctCustomers = new HashSet<ProudctCustomer>();
        }

        public decimal Proudctid { get; set; }
        public string Name { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Price { get; set; }
        public decimal? Categoryid { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProudctCustomer> ProudctCustomers { get; set; }
    }
}
