using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_costcenter")]
    public partial class TmCostcenter
    {
        public TmCostcenter()
        {
            InverseGprtCcCodeNavigation = new HashSet<TmCostcenter>();
            InversePrtCcCodeNavigation = new HashSet<TmCostcenter>();
        }

        [Key]
        [StringLength(10)]
        public string CcCode { get; set; }
        [Required]
        [StringLength(35)]
        public string CcName { get; set; }
        [StringLength(10)]
        public string PrtCcCode { get; set; }
        [StringLength(10)]
        public string GprtCcCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CcType { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(10)]
        public string RegionCode { get; set; }

        [ForeignKey(nameof(GprtCcCode))]
        [InverseProperty(nameof(TmCostcenter.InverseGprtCcCodeNavigation))]
        public virtual TmCostcenter GprtCcCodeNavigation { get; set; }
        [ForeignKey(nameof(PrtCcCode))]
        [InverseProperty(nameof(TmCostcenter.InversePrtCcCodeNavigation))]
        public virtual TmCostcenter PrtCcCodeNavigation { get; set; }
        [ForeignKey(nameof(RegionCode))]
        [InverseProperty(nameof(TmRegion.TmCostcenter))]
        public virtual TmRegion RegionCodeNavigation { get; set; }
        [InverseProperty(nameof(TmCostcenter.GprtCcCodeNavigation))]
        public virtual ICollection<TmCostcenter> InverseGprtCcCodeNavigation { get; set; }
        [InverseProperty(nameof(TmCostcenter.PrtCcCodeNavigation))]
        public virtual ICollection<TmCostcenter> InversePrtCcCodeNavigation { get; set; }
    }
}
