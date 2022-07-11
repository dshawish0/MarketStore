using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectTahalf.Models
{
    public partial class ProjectCreditcard
    {
        public decimal Creditcardid { get; set; }
        public decimal Cardnumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Userid { get; set; }

        public virtual ProjectUser User { get; set; }
    }
}
