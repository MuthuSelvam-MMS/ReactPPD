using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_acgroup")]
    public partial class TmAcgroup
    {
        public TmAcgroup()
        {
            InversePrtGrpCodeNavigation = new HashSet<TmAcgroup>();
            TmAccountsConsGrpCodeCrNavigation = new HashSet<TmAccounts>();
            TmAccountsConsGrpCodeDrNavigation = new HashSet<TmAccounts>();
            TmAccountsGrpCodeCrNavigation = new HashSet<TmAccounts>();
            TmAccountsGrpCodeDrNavigation = new HashSet<TmAccounts>();
        }

        [Key]
        [StringLength(10)]
        public string GrpCode { get; set; }
        [Required]
        [StringLength(60)]
        public string GrpName { get; set; }
        [StringLength(10)]
        public string PrtGrpCode { get; set; }
        [Required]
        [StringLength(2)]
        public string GrpType { get; set; }
        [Column(TypeName = "int(6)")]
        public int? GrpOrder { get; set; }
        [Required]
        [StringLength(1)]
        public string SchReq { get; set; }
        [Column(TypeName = "int(3)")]
        public int SchNo { get; set; }
        [Required]
        [StringLength(1)]
        public string ExpandGrp { get; set; }
        [Required]
        [StringLength(2)]
        public string UsageType { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(PrtGrpCode))]
        [InverseProperty(nameof(TmAcgroup.InversePrtGrpCodeNavigation))]
        public virtual TmAcgroup PrtGrpCodeNavigation { get; set; }
        [ForeignKey(nameof(SchNo))]
        [InverseProperty(nameof(TmGrpschedule.TmAcgroup))]
        public virtual TmGrpschedule SchNoNavigation { get; set; }
        [InverseProperty(nameof(TmAcgroup.PrtGrpCodeNavigation))]
        public virtual ICollection<TmAcgroup> InversePrtGrpCodeNavigation { get; set; }
        [InverseProperty(nameof(TmAccounts.ConsGrpCodeCrNavigation))]
        public virtual ICollection<TmAccounts> TmAccountsConsGrpCodeCrNavigation { get; set; }
        [InverseProperty(nameof(TmAccounts.ConsGrpCodeDrNavigation))]
        public virtual ICollection<TmAccounts> TmAccountsConsGrpCodeDrNavigation { get; set; }
        [InverseProperty(nameof(TmAccounts.GrpCodeCrNavigation))]
        public virtual ICollection<TmAccounts> TmAccountsGrpCodeCrNavigation { get; set; }
        [InverseProperty(nameof(TmAccounts.GrpCodeDrNavigation))]
        public virtual ICollection<TmAccounts> TmAccountsGrpCodeDrNavigation { get; set; }
    }
}
