using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectTestimonial
    {
        public string Rating { get; set; }
        public decimal TestId { get; set; }
        public string Message { get; set; }
        public decimal Status { get; set; }
        public DateTime Publishdate { get; set; }
        public decimal Userid { get; set; }
        public decimal? Productid { get; set; }

        public virtual ProjectProduct Product { get; set; }
        public virtual ProjectUser User { get; set; }
    }
}
