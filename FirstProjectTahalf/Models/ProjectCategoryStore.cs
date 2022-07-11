using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectCategoryStore
    {
        public decimal CategoryStore { get; set; }
        public decimal? Storeid { get; set; }
        public decimal? Catid { get; set; }

        public virtual ProjectCategory Cat { get; set; }
        public virtual ProjectStore Store { get; set; }
    }
}
