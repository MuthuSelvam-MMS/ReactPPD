using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_acbrmap")]
    public partial class TmAcbrmap
    {
        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastTransDate { get; set; }
        [Required]
        [StringLength(1)]
        public string IsBudgetable { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(1)]
        public string IsTrans { get; set; }
        [StringLength(12)]
        public string BrShortName { get; set; }
        [Required]
        [StringLength(60)]
        public string AccountName { get; set; }
        [StringLength(2)]
        public string Actype { get; set; }
        [StringLength(1)]
        public string BroSal { get; set; }

        public virtual TmAccounts Ac { get; set; }
        [ForeignKey(nameof(AccountCode))]
        [InverseProperty(nameof(TmAccountsdetail.TmAcbrmap))]
        public virtual TmAccountsdetail AccountCode1 { get; set; }
        [ForeignKey(nameof(AccountCode))]
        [InverseProperty(nameof(TmAccounts.TmAcbrmapAccountCodeNavigation))]
        public virtual TmAccounts AccountCodeNavigation { get; set; }
        public virtual TmAccounts AccountNameNavigation { get; set; }
        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmAcbrmap))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
    }
}
