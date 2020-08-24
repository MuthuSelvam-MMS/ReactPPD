using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_item")]
    public partial class TmItem
    {
        public TmItem()
        {
            InverseContCode1Navigation = new HashSet<TmItem>();
            InverseContCode2Navigation = new HashSet<TmItem>();
            InversePmxCode1Navigation = new HashSet<TmItem>();
            InversePmxCode2Navigation = new HashSet<TmItem>();
            InversePrtItemNavigation = new HashSet<TmItem>();
            TmItemnature = new HashSet<TmItemnature>();
            TmVendor = new HashSet<TmVendor>();
        }

        [Key]
        [StringLength(10)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }
        [StringLength(150)]
        public string ItemSpec { get; set; }
        [StringLength(20)]
        public string PartNo { get; set; }
        [Required]
        [StringLength(10)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(20)]
        public string ItemType { get; set; }
        [Required]
        [StringLength(10)]
        public string Category { get; set; }
        [Required]
        [StringLength(12)]
        public string ItemGroup { get; set; }
        [StringLength(10)]
        public string PrtItem { get; set; }
        [Required]
        [StringLength(5)]
        public string UomBig { get; set; }
        [Required]
        [StringLength(5)]
        public string UomSmall { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal UomRelation { get; set; }
        [Required]
        [StringLength(10)]
        public string UomPur { get; set; }
        [Required]
        [StringLength(10)]
        public string UomStk { get; set; }
        [Required]
        [StringLength(20)]
        public string VisInSpn { get; set; }
        [Required]
        [StringLength(1)]
        public string InSpn { get; set; }
        [StringLength(1)]
        public string CashPur { get; set; }
        [StringLength(1)]
        public string Fsn { get; set; }
        [StringLength(1)]
        public string Abc { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string StProdCode { get; set; }
        [StringLength(10)]
        public string ContCode1 { get; set; }
        [StringLength(10)]
        public string ContCode2 { get; set; }
        [StringLength(10)]
        public string PmxCode1 { get; set; }
        [StringLength(10)]
        public string PmxCode2 { get; set; }
        [StringLength(25)]
        public string ProdNature { get; set; }
        [StringLength(1)]
        public string Cess { get; set; }
        [StringLength(1)]
        public string Nature { get; set; }
        [StringLength(10)]
        public string Div { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? ContCap { get; set; }
        [StringLength(1)]
        public string ContWtConf { get; set; }
        [Column(TypeName = "int(4)")]
        public int? ExpDays { get; set; }
        [StringLength(10)]
        public string InReq { get; set; }
        [StringLength(10)]
        public string AcCode { get; set; }
        [StringLength(1)]
        public string AcPostBase { get; set; }
        [StringLength(10)]
        public string Division { get; set; }
        [StringLength(10)]
        public string HsnCode { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Doses { get; set; }
        [Column(TypeName = "int(11)")]
        public int? NoOfPackets { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? StdWt { get; set; }

        [ForeignKey(nameof(Category))]
        [InverseProperty(nameof(TmItemcategory.TmItem))]
        public virtual TmItemcategory CategoryNavigation { get; set; }
        [ForeignKey(nameof(ContCode1))]
        [InverseProperty(nameof(TmItem.InverseContCode1Navigation))]
        public virtual TmItem ContCode1Navigation { get; set; }
        [ForeignKey(nameof(ContCode2))]
        [InverseProperty(nameof(TmItem.InverseContCode2Navigation))]
        public virtual TmItem ContCode2Navigation { get; set; }
        [ForeignKey(nameof(PmxCode1))]
        [InverseProperty(nameof(TmItem.InversePmxCode1Navigation))]
        public virtual TmItem PmxCode1Navigation { get; set; }
        [ForeignKey(nameof(PmxCode2))]
        [InverseProperty(nameof(TmItem.InversePmxCode2Navigation))]
        public virtual TmItem PmxCode2Navigation { get; set; }
        [ForeignKey(nameof(ProdNature))]
        [InverseProperty(nameof(TmProdnature.TmItem))]
        public virtual TmProdnature ProdNatureNavigation { get; set; }
        [ForeignKey(nameof(PrtItem))]
        [InverseProperty(nameof(TmItem.InversePrtItemNavigation))]
        public virtual TmItem PrtItemNavigation { get; set; }
        [ForeignKey(nameof(UomBig))]
        [InverseProperty(nameof(TmUom.TmItemUomBigNavigation))]
        public virtual TmUom UomBigNavigation { get; set; }
        [ForeignKey(nameof(UomPur))]
        [InverseProperty(nameof(TmUom.TmItemUomPurNavigation))]
        public virtual TmUom UomPurNavigation { get; set; }
        [ForeignKey(nameof(UomSmall))]
        [InverseProperty(nameof(TmUom.TmItemUomSmallNavigation))]
        public virtual TmUom UomSmallNavigation { get; set; }
        [ForeignKey(nameof(UomStk))]
        [InverseProperty(nameof(TmUom.TmItemUomStkNavigation))]
        public virtual TmUom UomStkNavigation { get; set; }
        [InverseProperty(nameof(TmItem.ContCode1Navigation))]
        public virtual ICollection<TmItem> InverseContCode1Navigation { get; set; }
        [InverseProperty(nameof(TmItem.ContCode2Navigation))]
        public virtual ICollection<TmItem> InverseContCode2Navigation { get; set; }
        [InverseProperty(nameof(TmItem.PmxCode1Navigation))]
        public virtual ICollection<TmItem> InversePmxCode1Navigation { get; set; }
        [InverseProperty(nameof(TmItem.PmxCode2Navigation))]
        public virtual ICollection<TmItem> InversePmxCode2Navigation { get; set; }
        [InverseProperty(nameof(TmItem.PrtItemNavigation))]
        public virtual ICollection<TmItem> InversePrtItemNavigation { get; set; }
        [InverseProperty("ItemCodeNavigation")]
        public virtual ICollection<TmItemnature> TmItemnature { get; set; }
        [InverseProperty("ItemCodeNavigation")]
        public virtual ICollection<TmVendor> TmVendor { get; set; }
    }
}
