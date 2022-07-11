using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class Role
    {
        public Role()
        {
            UserLogs = new HashSet<UserLog>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}
