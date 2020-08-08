using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_itemtype")]
    public partial class TmItemtype
    {
        [Key]
        [StringLength(5)]
        public string ItemType { get; set; }
        [Required]
        [StringLength(35)]
        public string Descn { get; set; }
        [Required]
        [StringLength(12)]
        public string PurAcWs { get; set; }
        [Required]
        [StringLength(12)]
        public string PurAcOs { get; set; }
        [Required]
        [StringLength(12)]
        public string SaleAcWs { get; set; }
        [Required]
        [StringLength(12)]
        public string SaleAcOs { get; set; }
        [Required]
        [StringLength(12)]
        public string StInAc { get; set; }
        [Required]
        [StringLength(12)]
        public string StOutAc { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(12)]
        public string RoundOff { get; set; }
        [StringLength(12)]
        public string PackingAc { get; set; }
        [StringLength(12)]
        public string FrtInAc { get; set; }
        [StringLength(12)]
        public string FrtOutAc { get; set; }
        [StringLength(12)]
        public string OthDedAc { get; set; }
        [Required]
        [StringLength(1)]
        public string Nature { get; set; }
        [StringLength(12)]
        public string UnLoadAc { get; set; }
        [StringLength(12)]
        public string WmtAc { get; set; }
        [StringLength(12)]
        public string CessAc { get; set; }
        [StringLength(12)]
        public string QtyCutAc { get; set; }
        [StringLength(12)]
        public string QltyCutAc { get; set; }
        [StringLength(12)]
        public string DamgUnAc { get; set; }
        [StringLength(12)]
        public string ExgUnAc { get; set; }
        [StringLength(12)]
        public string RateCutAc { get; set; }
        [StringLength(12)]
        public string NetWtCutAc { get; set; }
        [StringLength(12)]
        public string StinDivAc { get; set; }
        [StringLength(12)]
        public string StOutDivAc { get; set; }

        [ForeignKey(nameof(FrtInAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeFrtInAcNavigation))]
        public virtual TmAccounts FrtInAcNavigation { get; set; }
        [ForeignKey(nameof(FrtOutAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeFrtOutAcNavigation))]
        public virtual TmAccounts FrtOutAcNavigation { get; set; }
        [ForeignKey(nameof(OthDedAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeOthDedAcNavigation))]
        public virtual TmAccounts OthDedAcNavigation { get; set; }
        [ForeignKey(nameof(PackingAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypePackingAcNavigation))]
        public virtual TmAccounts PackingAcNavigation { get; set; }
        [ForeignKey(nameof(PurAcOs))]
        [InverseProperty(nameof(TmAccounts.TmItemtypePurAcOsNavigation))]
        public virtual TmAccounts PurAcOsNavigation { get; set; }
        [ForeignKey(nameof(PurAcWs))]
        [InverseProperty(nameof(TmAccounts.TmItemtypePurAcWsNavigation))]
        public virtual TmAccounts PurAcWsNavigation { get; set; }
        [ForeignKey(nameof(RoundOff))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeRoundOffNavigation))]
        public virtual TmAccounts RoundOffNavigation { get; set; }
        [ForeignKey(nameof(SaleAcOs))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeSaleAcOsNavigation))]
        public virtual TmAccounts SaleAcOsNavigation { get; set; }
        [ForeignKey(nameof(SaleAcWs))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeSaleAcWsNavigation))]
        public virtual TmAccounts SaleAcWsNavigation { get; set; }
        [ForeignKey(nameof(StInAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeStInAcNavigation))]
        public virtual TmAccounts StInAcNavigation { get; set; }
        [ForeignKey(nameof(StOutAc))]
        [InverseProperty(nameof(TmAccounts.TmItemtypeStOutAcNavigation))]
        public virtual TmAccounts StOutAcNavigation { get; set; }
    }
}
