using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_vehicle")]
    public partial class TmVehicle
    {
        [Key]
        [StringLength(10)]
        public string VehCode { get; set; }
        [Required]
        [StringLength(20)]
        public string VehNo { get; set; }
        [Required]
        [StringLength(10)]
        public string VehType { get; set; }
        [Required]
        [StringLength(35)]
        public string MakeName { get; set; }
        [Required]
        [StringLength(10)]
        public string DivCode { get; set; }
        [Required]
        [StringLength(10)]
        public string RegionCode { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(RegionCode))]
        [InverseProperty(nameof(TmRegion.TmVehicle))]
        public virtual TmRegion RegionCodeNavigation { get; set; }
    }
}
