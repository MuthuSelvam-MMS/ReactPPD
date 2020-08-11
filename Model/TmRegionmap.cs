using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_regionmap")]
    public partial class TmRegionmap
    {
        [Key]
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [Key]
        [StringLength(10)]
        public string DivCode { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(CompanyCode))]
        [InverseProperty(nameof(TmCompany.TmRegionmap))]
        public virtual TmCompany CompanyCodeNavigation { get; set; }
        [ForeignKey(nameof(DivCode))]
        [InverseProperty(nameof(TmDivision.TmRegionmap))]
        public virtual TmDivision DivCodeNavigation { get; set; }
        [ForeignKey(nameof(RegZoneCode))]
        [InverseProperty(nameof(TmRegzone.TmRegionmap))]
        public virtual TmRegzone RegZoneCodeNavigation { get; set; }
        [ForeignKey(nameof(RegionCode))]
        [InverseProperty(nameof(TmRegion.TmRegionmap))]
        public virtual TmRegion RegionCodeNavigation { get; set; }
    }
}
