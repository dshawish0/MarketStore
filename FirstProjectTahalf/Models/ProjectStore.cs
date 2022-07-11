using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectStore
    {
        public ProjectStore()
        {
            ProjectCategoryStores = new HashSet<ProjectCategoryStore>();
            ProjectProducts = new HashSet<ProjectProduct>();
        }

        public decimal Storeid { get; set; }
        public string Namestore { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Opentime { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        public virtual IFormFile ImgFile { get; set; }

        public virtual ICollection<ProjectCategoryStore> ProjectCategoryStores { get; set; }
        public virtual ICollection<ProjectProduct> ProjectProducts { get; set; }
    }
}
