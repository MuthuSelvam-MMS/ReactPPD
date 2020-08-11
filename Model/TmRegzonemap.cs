using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_regzonemap")]
    public partial class TmRegzonemap
    {
        [Key]
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [Key]
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [Key]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Key]
        [StringLength(10)]
        public string DivCode { get; set; }

        [ForeignKey(nameof(CompanyCode))]
        [InverseProperty(nameof(TmCompany.TmRegzonemap))]
        public virtual TmCompany CompanyCodeNavigation { get; set; }
        [ForeignKey(nameof(DivCode))]
        [InverseProperty(nameof(TmDivision.TmRegzonemap))]
        public virtual TmDivision DivCodeNavigation { get; set; }
        [ForeignKey(nameof(RegZoneCode))]
        [InverseProperty(nameof(TmRegzone.TmRegzonemap))]
        public virtual TmRegzone RegZoneCodeNavigation { get; set; }
    }
}
