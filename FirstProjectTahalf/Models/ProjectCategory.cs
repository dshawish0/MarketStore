using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectCategory
    {
        public ProjectCategory()
        {
            ProjectCategoryStores = new HashSet<ProjectCategoryStore>();
            ProjectProducts = new HashSet<ProjectProduct>();
        }

        public decimal Categoryid { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public virtual IFormFile ImgFileCategory { get; set; }
        public virtual ICollection<ProjectCategoryStore> ProjectCategoryStores { get; set; }
        public virtual ICollection<ProjectProduct> ProjectProducts { get; set; }
    }
}
