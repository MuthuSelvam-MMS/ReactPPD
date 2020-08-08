using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_itemcategory")]
    public partial class TmItemcategory
    {
        public TmItemcategory()
        {
            InverseGprtCodeNavigation = new HashSet<TmItemcategory>();
            InversePrtCodeNavigation = new HashSet<TmItemcategory>();
            TmItem = new HashSet<TmItem>();
        }

        [Key]
        [StringLength(10)]
        public string CatgyCode { get; set; }
        [StringLength(35)]
        public string CatgyName { get; set; }
        [Required]
        [StringLength(10)]
        public string PrtCode { get; set; }
        [Required]
        [StringLength(10)]
        public string GprtCode { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(12)]
        public string AssetAcCode { get; set; }
        [StringLength(12)]
        public string DepResCode { get; set; }
        [StringLength(12)]
        public string SaleProfitCode { get; set; }
        [StringLength(12)]
        public string SaleLossCode { get; set; }
        [StringLength(12)]
        public string CapWipCode { get; set; }
        [StringLength(12)]
        public string PreOprExpCode { get; set; }
        [StringLength(12)]
        public string DiscardLossCode { get; set; }
        [StringLength(12)]
        public string CapSubCode { get; set; }
        [StringLength(12)]
        public string AdvImpCode { get; set; }
        [StringLength(12)]
        public string RevResCode { get; set; }
        [StringLength(12)]
        public string LeaseCode { get; set; }
        [StringLength(12)]
        public string DepMethod { get; set; }
        [StringLength(12)]
        public string DepNor { get; set; }
        [StringLength(12)]
        public string DepSingle { get; set; }
        [StringLength(12)]
        public string DepDouble { get; set; }
        [StringLength(1)]
        public string IsAsset { get; set; }
        [StringLength(1)]
        public string AssetPreFix { get; set; }

        [ForeignKey(nameof(GprtCode))]
        [InverseProperty(nameof(TmItemcategory.InverseGprtCodeNavigation))]
        public virtual TmItemcategory GprtCodeNavigation { get; set; }
        [ForeignKey(nameof(PrtCode))]
        [InverseProperty(nameof(TmItemcategory.InversePrtCodeNavigation))]
        public virtual TmItemcategory PrtCodeNavigation { get; set; }
        [InverseProperty(nameof(TmItemcategory.GprtCodeNavigation))]
        public virtual ICollection<TmItemcategory> InverseGprtCodeNavigation { get; set; }
        [InverseProperty(nameof(TmItemcategory.PrtCodeNavigation))]
        public virtual ICollection<TmItemcategory> InversePrtCodeNavigation { get; set; }
        [InverseProperty("CategoryNavigation")]
        public virtual ICollection<TmItem> TmItem { get; set; }
    }
}
