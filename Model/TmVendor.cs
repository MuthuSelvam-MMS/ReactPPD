using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_vendor")]
    public partial class TmVendor
    {
        [Key]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [Key]
        [StringLength(10)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(3)]
        public string PurType { get; set; }

        [ForeignKey(nameof(AccountCode))]
        [InverseProperty(nameof(TmAccountsdetail.TmVendor))]
        public virtual TmAccountsdetail AccountCode1 { get; set; }
        [ForeignKey(nameof(AccountCode))]
        [InverseProperty(nameof(TmAccounts.TmVendor))]
        public virtual TmAccounts AccountCodeNavigation { get; set; }
        [ForeignKey(nameof(ItemCode))]
        [InverseProperty(nameof(TmItem.TmVendor))]
        public virtual TmItem ItemCodeNavigation { get; set; }
    }
}
