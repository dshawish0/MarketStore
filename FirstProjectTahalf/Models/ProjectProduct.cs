using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectProduct
    {
        public ProjectProduct()
        {
            ProjectProductOrders = new HashSet<ProjectProductOrder>();
            ProjectTestimonials = new HashSet<ProjectTestimonial>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public string ImagePath { get; set; }
        public decimal Quntitiy { get; set; }
        public decimal Propricse { get; set; }
        public decimal Prosale { get; set; }
        public decimal Discount { get; set; }
        public string Productdescription { get; set; }
        public decimal Storeid { get; set; }
        public decimal Categoryid { get; set; }

        [NotMapped]
        public virtual IFormFile ImgFile { get; set; }

        public virtual ProjectCategory Category { get; set; }
        public virtual ProjectStore Store { get; set; }
        public virtual ICollection<ProjectProductOrder> ProjectProductOrders { get; set; }
        public virtual ICollection<ProjectTestimonial> ProjectTestimonials { get; set; }
    }
}
