using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_userdefault")]
    public partial class TmUserdefault
    {
        [Key]
        [StringLength(12)]
        public string UserId { get; set; }
        [StringLength(40)]
        public string UserName { get; set; }
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [StringLength(10)]
        public string DivisionCode { get; set; }
        [StringLength(35)]
        public string DivisionName { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(10)]
        public string BranchCode { get; set; }
        [StringLength(35)]
        public string BranchName { get; set; }
        [StringLength(10)]
        public string BranchState { get; set; }
        [Column(TypeName = "int(2)")]
        public int? YearNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? YearStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? YearEnd { get; set; }
        [Column("UPassword")]
        [StringLength(10)]
        public string Upassword { get; set; }
        [StringLength(15)]
        public string SessionId { get; set; }
        [Column(TypeName = "int(4)")]
        public int? StartYearNo { get; set; }
        [Column(TypeName = "int(4)")]
        public int? EndYearNo { get; set; }
        [StringLength(1)]
        public string Logged { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(35)]
        public string RegZoneName { get; set; }

        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmUserdefault))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
        [ForeignKey(nameof(DivisionCode))]
        [InverseProperty(nameof(TmDivision.TmUserdefault))]
        public virtual TmDivision DivisionCodeNavigation { get; set; }
        [ForeignKey(nameof(RegZoneCode))]
        [InverseProperty(nameof(TmRegzone.TmUserdefault))]
        public virtual TmRegzone RegZoneCodeNavigation { get; set; }
        [ForeignKey(nameof(RegionCode))]
        [InverseProperty(nameof(TmRegion.TmUserdefault))]
        public virtual TmRegion RegionCodeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmEmployee.TmUserdefault))]
        public virtual TmEmployee User { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmUser.TmUserdefault))]
        public virtual TmUser UserNavigation { get; set; }
    }
}
