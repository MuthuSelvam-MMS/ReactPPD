using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_zone")]
    public partial class TmZone
    {
        [Key]
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(20)]
        public string ZoneName { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string ShortName { get; set; }
        [StringLength(10)]
        public string ZoneHeadCode { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string ZoneBrCode { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }

        [ForeignKey(nameof(RegZoneCode))]
        [InverseProperty(nameof(TmRegzone.TmZone))]
        public virtual TmRegzone RegZoneCodeNavigation { get; set; }
        [ForeignKey(nameof(RegionCode))]
        [InverseProperty(nameof(TmRegion.TmZone))]
        public virtual TmRegion RegionCodeNavigation { get; set; }
        [ForeignKey(nameof(ZoneBrCode))]
        [InverseProperty(nameof(TmBranch.TmZone))]
        public virtual TmBranch ZoneBrCodeNavigation { get; set; }
        [ForeignKey(nameof(ZoneHeadCode))]
        [InverseProperty(nameof(TmEmployee.TmZone))]
        public virtual TmEmployee ZoneHeadCodeNavigation { get; set; }
    }
}
