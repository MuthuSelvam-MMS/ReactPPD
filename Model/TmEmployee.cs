using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_employee")]
    public partial class TmEmployee
    {
        public TmEmployee()
        {
            TmRegion = new HashSet<TmRegion>();
            TmUserdivmap = new HashSet<TmUserdivmap>();
            TmUserright = new HashSet<TmUserright>();
            TmZone = new HashSet<TmZone>();
        }

        [Key]
        [StringLength(10)]
        public string EmpCode { get; set; }
        [StringLength(10)]
        public string InteCode { get; set; }
        [StringLength(10)]
        public string PaCode { get; set; }
        [StringLength(40)]
        public string EmpName { get; set; }
        [StringLength(10)]
        public string BranchCode { get; set; }
        [StringLength(3)]
        public string EmpType { get; set; }
        [StringLength(3)]
        public string EmpCategory { get; set; }
        [StringLength(3)]
        public string Sex { get; set; }
        [StringLength(10)]
        public string MartialStatus { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DobCelebrate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Doj { get; set; }
        [StringLength(3)]
        public string IsWorking { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [StringLength(25)]
        public string PfNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PfElgDate { get; set; }
        [StringLength(50)]
        public string EsIno { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EsIelgDate { get; set; }
        [StringLength(50)]
        public string PanNo { get; set; }
        [StringLength(25)]
        public string SalaryAcNo { get; set; }
        [StringLength(10)]
        public string SalaryAcBank { get; set; }
        [StringLength(3)]
        public string SalPayMode { get; set; }
        [StringLength(25)]
        public string LicenseNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LicenseUpTo { get; set; }
        [StringLength(25)]
        public string PassPortNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PassPortUpTo { get; set; }
        [StringLength(3)]
        public string Religion { get; set; }
        [StringLength(3)]
        public string Nationality { get; set; }
        [StringLength(10)]
        public string BloodGroup { get; set; }
        [StringLength(3)]
        public string JoinAs { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ProBfrDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ProBtoDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ConfirmDate { get; set; }
        [StringLength(25)]
        public string GraduatyNo { get; set; }
        [StringLength(25)]
        public string SupanuationNo { get; set; }
        [StringLength(3)]
        public string SwfContr { get; set; }
        [StringLength(25)]
        public string SwfContrNo { get; set; }
        [StringLength(3)]
        public string LwfContr { get; set; }
        [StringLength(25)]
        public string LwfContrNo { get; set; }
        [StringLength(40)]
        public string IceContact { get; set; }
        [Column(TypeName = "int(15)")]
        public int? IcePhoneNo { get; set; }
        [StringLength(50)]
        public string MailIdOff { get; set; }
        [StringLength(50)]
        public string MailIdPer { get; set; }
        [StringLength(15)]
        public string MobileOff { get; set; }
        [StringLength(10)]
        public string MobileNet { get; set; }
        [Column(TypeName = "int(2)")]
        public int? TotExpYear { get; set; }
        [Column(TypeName = "int(2)")]
        public int? TotExpMonth { get; set; }
        [StringLength(10)]
        public string Substitute { get; set; }
        [StringLength(10)]
        public string FunCrepHead { get; set; }
        [StringLength(10)]
        public string AdmRepHead { get; set; }
        [StringLength(10)]
        public string Vehclass { get; set; }
        [StringLength(12)]
        public string VehNo { get; set; }
        [Column("WOff")]
        [StringLength(3)]
        public string Woff { get; set; }
        [StringLength(10)]
        public string ShiftType { get; set; }
        [Column(TypeName = "int(2)")]
        public int? CfelDays { get; set; }
        [Column(TypeName = "int(2)")]
        public int? ElgelDays { get; set; }
        [Column(TypeName = "int(2)")]
        public int? ElgclDays { get; set; }
        [Column(TypeName = "int(2)")]
        public int? BalelDays { get; set; }
        [Column(TypeName = "int(2)")]
        public int? BalclDays { get; set; }
        [Column(TypeName = "date")]
        public DateTime? RelivDate { get; set; }
        [Column(TypeName = "int(15)")]
        public int? DocIntNo { get; set; }
        [StringLength(15)]
        public string DocNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DocDate { get; set; }
        [StringLength(10)]
        public string Caste { get; set; }
        [StringLength(10)]
        public string VehType { get; set; }
        [StringLength(3)]
        public string SalProcess { get; set; }
        [StringLength(10)]
        public string SubBrCode { get; set; }
        [StringLength(10)]
        public string LeaReason { get; set; }
        [StringLength(25)]
        public string OldPfNo { get; set; }
        [StringLength(20)]
        public string PfSsnNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PfssnDate { get; set; }
        [StringLength(3)]
        public string LastSalDrawn { get; set; }

        [ForeignKey(nameof(AdmRepHead))]
        [InverseProperty(nameof(TmDesgn.TmEmployeeAdmRepHeadNavigation))]
        public virtual TmDesgn AdmRepHeadNavigation { get; set; }
        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmEmployeeBranchCodeNavigation))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
        [ForeignKey(nameof(JoinAs))]
        [InverseProperty(nameof(TmDesgn.TmEmployeeJoinAsNavigation))]
        public virtual TmDesgn JoinAsNavigation { get; set; }
        [ForeignKey(nameof(SubBrCode))]
        [InverseProperty(nameof(TmBranch.TmEmployeeSubBrCodeNavigation))]
        public virtual TmBranch SubBrCodeNavigation { get; set; }
        [ForeignKey(nameof(Substitute))]
        [InverseProperty(nameof(TmDesgn.TmEmployeeSubstituteNavigation))]
        public virtual TmDesgn SubstituteNavigation { get; set; }
        [InverseProperty("User")]
        public virtual TmUserdefault TmUserdefault { get; set; }
        [InverseProperty("RegHeadCodeNavigation")]
        public virtual ICollection<TmRegion> TmRegion { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TmUserdivmap> TmUserdivmap { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TmUserright> TmUserright { get; set; }
        [InverseProperty("ZoneHeadCodeNavigation")]
        public virtual ICollection<TmZone> TmZone { get; set; }
    }
}
