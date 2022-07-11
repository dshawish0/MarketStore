using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectRole
    {
        public ProjectRole()
        {
            ProjectUsers = new HashSet<ProjectUser>();
        }

        public decimal Roleid { get; set; }
        public string Roletype { get; set; }

        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
