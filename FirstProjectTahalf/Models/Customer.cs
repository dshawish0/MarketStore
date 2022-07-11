using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class Customer
    {
        public Customer()
        {
            ProudctCustomers = new HashSet<ProudctCustomer>();
            UserLogs = new HashSet<UserLog>();
        }

        public decimal Customerid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<ProudctCustomer> ProudctCustomers { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}
