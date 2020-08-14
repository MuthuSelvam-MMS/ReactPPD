using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_accounts")]
    public partial class TmAccounts
    {
        public TmAccounts()
        {
            InverseControlAcNavigation = new HashSet<TmAccounts>();
            TmAcbrmapAc = new HashSet<TmAcbrmap>();
            TmAcbrmapAccountCodeNavigation = new HashSet<TmAcbrmap>();
            TmAcbrmapAccountNameNavigation = new HashSet<TmAcbrmap>();
            TmCompany = new HashSet<TmCompany>();
            TmDoctypesAcCode1Navigation = new HashSet<TmDoctypes>();
            TmDoctypesAcCodeNavigation = new HashSet<TmDoctypes>();
            TmItemtypeFrtInAcNavigation = new HashSet<TmItemtype>();
            TmItemtypeFrtOutAcNavigation = new HashSet<TmItemtype>();
            TmItemtypeOthDedAcNavigation = new HashSet<TmItemtype>();
            TmItemtypePackingAcNavigation = new HashSet<TmItemtype>();
            TmItemtypePurAcOsNavigation = new HashSet<TmItemtype>();
            TmItemtypePurAcWsNavigation = new HashSet<TmItemtype>();
            TmItemtypeRoundOffNavigation = new HashSet<TmItemtype>();
            TmItemtypeSaleAcOsNavigation = new HashSet<TmItemtype>();
            TmItemtypeSaleAcWsNavigation = new HashSet<TmItemtype>();
            TmItemtypeStInAcNavigation = new HashSet<TmItemtype>();
            TmItemtypeStOutAcNavigation = new HashSet<TmItemtype>();
            TmVendor = new HashSet<TmVendor>();
        }

        [Key]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }
        [Required]
        [StringLength(10)]
        public string GrpCodeCr { get; set; }
        [Required]
        [StringLength(10)]
        public string GrpCodeDr { get; set; }
        [Required]
        [StringLength(5)]
        public string AcType { get; set; }
        [Required]
        [StringLength(2)]
        public string GrpType { get; set; }
        [StringLength(12)]
        public string ControlAc { get; set; }
        [Required]
        [StringLength(2)]
        public string BillAlloc { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalCode1 { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalCode2 { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalCode3 { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalCode4 { get; set; }
        [Required]
        [StringLength(10)]
        public string AnalCode5 { get; set; }
        [Required]
        [StringLength(1)]
        public string CcReq { get; set; }
        [Required]
        [StringLength(10)]
        public string SubLedCategory { get; set; }
        [Required]
        [StringLength(10)]
        public string Currency { get; set; }
        [Required]
        [StringLength(1)]
        public string TransControl { get; set; }
        [Column(TypeName = "int(5)")]
        public int CreditDays { get; set; }
        [Required]
        [StringLength(2)]
        public string ExpPymt { get; set; }
        [Required]
        [StringLength(10)]
        public string AreaCode { get; set; }
        [Required]
        [StringLength(10)]
        public string ConsGrpCodeCr { get; set; }
        [Required]
        [StringLength(10)]
        public string ConsGrpCodeDr { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(1)]
        public string PartyType { get; set; }
        [StringLength(1)]
        public string AccNature { get; set; }
        [Required]
        [StringLength(25)]
        public string BeAcOnSl { get; set; }
        [StringLength(10)]
        public string BeAcOnGl { get; set; }
        [Required]
        [StringLength(10)]
        public string BeAcOnSlType { get; set; }
        [StringLength(100)]
        public string BeAcOnDesc { get; set; }
        [StringLength(1)]
        public string Tds { get; set; }
        [StringLength(10)]
        public string TdsNature { get; set; }
        [StringLength(1)]
        public string Exported { get; set; }
        [StringLength(1)]
        public string IsGovtOrg { get; set; }
        [StringLength(10)]
        public string EmpCode { get; set; }
        [StringLength(60)]
        public string PayFavour { get; set; }
        [StringLength(20)]
        public string PartyCode { get; set; }
        [StringLength(40)]
        public string PartyName { get; set; }

        [ForeignKey(nameof(AnalCode1))]
        [InverseProperty(nameof(TmAnalysis.TmAccountsAnalCode1Navigation))]
        public virtual TmAnalysis AnalCode1Navigation { get; set; }
        [ForeignKey(nameof(AnalCode2))]
        [InverseProperty(nameof(TmAnalysis.TmAccountsAnalCode2Navigation))]
        public virtual TmAnalysis AnalCode2Navigation { get; set; }
        [ForeignKey(nameof(AnalCode3))]
        [InverseProperty(nameof(TmAnalysis.TmAccountsAnalCode3Navigation))]
        public virtual TmAnalysis AnalCode3Navigation { get; set; }
        [ForeignKey(nameof(AnalCode4))]
        [InverseProperty(nameof(TmAnalysis.TmAccountsAnalCode4Navigation))]
        public virtual TmAnalysis AnalCode4Navigation { get; set; }
        [ForeignKey(nameof(AnalCode5))]
        [InverseProperty(nameof(TmAnalysis.TmAccountsAnalCode5Navigation))]
        public virtual TmAnalysis AnalCode5Navigation { get; set; }
        [ForeignKey(nameof(ConsGrpCodeCr))]
        [InverseProperty(nameof(TmAcgroup.TmAccountsConsGrpCodeCrNavigation))]
        public virtual TmAcgroup ConsGrpCodeCrNavigation { get; set; }
        [ForeignKey(nameof(ConsGrpCodeDr))]
        [InverseProperty(nameof(TmAcgroup.TmAccountsConsGrpCodeDrNavigation))]
        public virtual TmAcgroup ConsGrpCodeDrNavigation { get; set; }
        [ForeignKey(nameof(ControlAc))]
        [InverseProperty(nameof(TmAccounts.InverseControlAcNavigation))]
        public virtual TmAccounts ControlAcNavigation { get; set; }
        [ForeignKey(nameof(GrpCodeCr))]
        [InverseProperty(nameof(TmAcgroup.TmAccountsGrpCodeCrNavigation))]
        public virtual TmAcgroup GrpCodeCrNavigation { get; set; }
        [ForeignKey(nameof(GrpCodeDr))]
        [InverseProperty(nameof(TmAcgroup.TmAccountsGrpCodeDrNavigation))]
        public virtual TmAcgroup GrpCodeDrNavigation { get; set; }
        [ForeignKey(nameof(PartyType))]
        [InverseProperty(nameof(TmPartytype.TmAccounts))]
        public virtual TmPartytype PartyTypeNavigation { get; set; }
        [InverseProperty("AccountCodeNavigation")]
        public virtual TmAccountsdetail TmAccountsdetail { get; set; }
        [InverseProperty(nameof(TmAccounts.ControlAcNavigation))]
        public virtual ICollection<TmAccounts> InverseControlAcNavigation { get; set; }
        public virtual ICollection<TmAcbrmap> TmAcbrmapAc { get; set; }
        [InverseProperty(nameof(TmAcbrmap.AccountCodeNavigation))]
        public virtual ICollection<TmAcbrmap> TmAcbrmapAccountCodeNavigation { get; set; }
        public virtual ICollection<TmAcbrmap> TmAcbrmapAccountNameNavigation { get; set; }
        [InverseProperty("GcacCodeNavigation")]
        public virtual ICollection<TmCompany> TmCompany { get; set; }
        [InverseProperty(nameof(TmDoctypes.AcCode1Navigation))]
        public virtual ICollection<TmDoctypes> TmDoctypesAcCode1Navigation { get; set; }
        [InverseProperty(nameof(TmDoctypes.AcCodeNavigation))]
        public virtual ICollection<TmDoctypes> TmDoctypesAcCodeNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.FrtInAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeFrtInAcNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.FrtOutAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeFrtOutAcNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.OthDedAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeOthDedAcNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.PackingAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypePackingAcNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.PurAcOsNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypePurAcOsNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.PurAcWsNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypePurAcWsNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.RoundOffNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeRoundOffNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.SaleAcOsNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeSaleAcOsNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.SaleAcWsNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeSaleAcWsNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.StInAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeStInAcNavigation { get; set; }
        [InverseProperty(nameof(TmItemtype.StOutAcNavigation))]
        public virtual ICollection<TmItemtype> TmItemtypeStOutAcNavigation { get; set; }
        [InverseProperty("AccountCodeNavigation")]
        public virtual ICollection<TmVendor> TmVendor { get; set; }
    }
}
