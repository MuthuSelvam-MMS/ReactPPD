using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_partytype")]
    public partial class TmPartytype
    {
        public TmPartytype()
        {
            TmAccounts = new HashSet<TmAccounts>();
        }

        [Key]
        [StringLength(2)]
        public string PartyType { get; set; }
        [StringLength(30)]
        public string Descn { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("PartyTypeNavigation")]
        public virtual ICollection<TmAccounts> TmAccounts { get; set; }
    }
}
