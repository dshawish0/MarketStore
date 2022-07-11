using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectHome
    {
        public ProjectHome()
        {
            ProjectUsers = new HashSet<ProjectUser>();
        }

        public decimal Homeid { get; set; }
        public string Email { get; set; }
        public string Freeshippinglimit { get; set; }
        public string Phonenumber { get; set; }
        public string Supportworktime { get; set; }
        public string Logoimg { get; set; }
        public string Homeimg { get; set; }
        public string Address { get; set; }

        [NotMapped]
        public virtual IFormFile ImgFileLogo { get; set; }

        [NotMapped]
        public virtual IFormFile ImgFileHome { get; set; }

        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
