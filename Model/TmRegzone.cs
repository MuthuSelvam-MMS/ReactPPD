using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_regzone")]
    public partial class TmRegzone
    {
        public TmRegzone()
        {
            TmRegion = new HashSet<TmRegion>();
            TmUserdefault = new HashSet<TmUserdefault>();
            TmZone = new HashSet<TmZone>();
        }

        [Key]
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(20)]
        public string RegZoneName { get; set; }
        [StringLength(10)]
        public string ShortName { get; set; }
        [StringLength(10)]
        public string RegZoneHead { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(3)]
        public string BeAcOnZc { get; set; }
        [Required]
        [StringLength(3)]
        public string BeAcOnLc { get; set; }
        [Required]
        [StringLength(10)]
        public string CmpId { get; set; }
        [Required]
        [StringLength(10)]
        public string RegZoneAcHead { get; set; }
        [Column(TypeName = "int(3)")]
        public int OrdNo { get; set; }
        [Required]
        [StringLength(10)]
        public string BcOnIdPfix { get; set; }
        [Required]
        [StringLength(10)]
        public string RegZoneBr { get; set; }
        [Required]
        [StringLength(10)]
        public string HrHead { get; set; }
        [Column(TypeName = "int(2)")]
        public int BconSysId { get; set; }

        [InverseProperty("RegZoneCodeNavigation")]
        public virtual ICollection<TmRegion> TmRegion { get; set; }
        [InverseProperty("RegZoneCodeNavigation")]
        public virtual ICollection<TmUserdefault> TmUserdefault { get; set; }
        [InverseProperty("RegZoneCodeNavigation")]
        public virtual ICollection<TmZone> TmZone { get; set; }
    }
}
