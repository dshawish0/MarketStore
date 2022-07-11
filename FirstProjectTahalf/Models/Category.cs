using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class Category
    {
        public Category()
        {
            Proudcts = new HashSet<Proudct>();
        }

        public decimal Categoryid { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Proudct> Proudcts { get; set; }
    }
}
