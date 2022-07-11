using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class UserLog
    {
        public decimal UserId { get; set; }
        public string Username { get; set; }
        public string Passowrd { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Role Role { get; set; }
    }
}
