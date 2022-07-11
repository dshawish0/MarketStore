using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectUser
    {
        public ProjectUser()
        {
            ProjectCreditcards = new HashSet<ProjectCreditcard>();
            ProjectOrders = new HashSet<ProjectOrder>();
            ProjectTestimonials = new HashSet<ProjectTestimonial>();
        }

        public decimal Userid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public decimal Roleid { get; set; }
        public decimal? Homeid { get; set; }

        [NotMapped]
        public virtual IFormFile ImgFileProfile { get; set; }

        public virtual ProjectHome Home { get; set; }
        public virtual ProjectRole Role { get; set; }
        public virtual ICollection<ProjectCreditcard> ProjectCreditcards { get; set; }
        public virtual ICollection<ProjectOrder> ProjectOrders { get; set; }
        public virtual ICollection<ProjectTestimonial> ProjectTestimonials { get; set; }
    }
}
