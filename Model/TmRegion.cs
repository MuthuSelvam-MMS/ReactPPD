using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_region")]
    public partial class TmRegion
    {
        public TmRegion()
        {
            TmCostcenter = new HashSet<TmCostcenter>();
            TmUser = new HashSet<TmUser>();
            TmUserdefault = new HashSet<TmUserdefault>();
            TmZone = new HashSet<TmZone>();
        }

        [Key]
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(30)]
        public string RegionName { get; set; }
        [StringLength(5)]
        public string ShortName { get; set; }
        [Column(TypeName = "int(5)")]
        public int? SlNo { get; set; }
        [StringLength(10)]
        public string RegHeadCode { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(10)]
        public string RobrCode { get; set; }

        [ForeignKey(nameof(RegHeadCode))]
        [InverseProperty(nameof(TmEmployee.TmRegion))]
        public virtual TmEmployee RegHeadCodeNavigation { get; set; }
        [ForeignKey(nameof(RegZoneCode))]
        [InverseProperty(nameof(TmRegzone.TmRegion))]
        public virtual TmRegzone RegZoneCodeNavigation { get; set; }
        [ForeignKey(nameof(RobrCode))]
        [InverseProperty(nameof(TmBranch.TmRegion))]
        public virtual TmBranch RobrCodeNavigation { get; set; }
        [InverseProperty("RegionCodeNavigation")]
        public virtual ICollection<TmCostcenter> TmCostcenter { get; set; }
        [InverseProperty("ReigionCodeNavigation")]
        public virtual ICollection<TmUser> TmUser { get; set; }
        [InverseProperty("RegionCodeNavigation")]
        public virtual ICollection<TmUserdefault> TmUserdefault { get; set; }
        [InverseProperty("RegionCodeNavigation")]
        public virtual ICollection<TmZone> TmZone { get; set; }
    }
}
