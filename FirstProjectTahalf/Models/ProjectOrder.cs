using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectOrder
    {
        public ProjectOrder()
        {
            ProjectPayments = new HashSet<ProjectPayment>();
            ProjectProductOrders = new HashSet<ProjectProductOrder>();
        }

        public decimal Orderid { get; set; }
        public DateTime? Datee { get; set; }
        public decimal? Total { get; set; }
        public decimal? Userid { get; set; }

        public virtual ProjectUser User { get; set; }
        public virtual ICollection<ProjectPayment> ProjectPayments { get; set; }
        public virtual ICollection<ProjectProductOrder> ProjectProductOrders { get; set; }
    }
}
