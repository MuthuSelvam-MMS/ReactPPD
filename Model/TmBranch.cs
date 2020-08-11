using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_branch")]
    public partial class TmBranch
    {
        public TmBranch()
        {
            TmAcbrmap = new HashSet<TmAcbrmap>();
            TmEmployeeBranchCodeNavigation = new HashSet<TmEmployee>();
            TmEmployeeSubBrCodeNavigation = new HashSet<TmEmployee>();
            TmRegion = new HashSet<TmRegion>();
            TmUserdefault = new HashSet<TmUserdefault>();
            TmUserright = new HashSet<TmUserright>();
            TmZone = new HashSet<TmZone>();
        }

        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [StringLength(50)]
        public string BranchName { get; set; }
        [StringLength(6)]
        public string BrType { get; set; }
        [StringLength(12)]
        public string ShortName { get; set; }
        [StringLength(40)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        [StringLength(40)]
        public string Address3 { get; set; }
        [StringLength(10)]
        public string PinCode { get; set; }
        [StringLength(10)]
        public string CityCode { get; set; }
        [StringLength(10)]
        public string TalukCode { get; set; }
        [StringLength(10)]
        public string DistCode { get; set; }
        [StringLength(10)]
        public string StateCode { get; set; }
        [StringLength(10)]
        public string CountryCode { get; set; }
        [StringLength(25)]
        public string MailId { get; set; }
        [StringLength(20)]
        public string PhoneNo { get; set; }
        [StringLength(15)]
        public string FaxNo { get; set; }
        [StringLength(15)]
        public string MobileNo { get; set; }
        [StringLength(20)]
        public string CstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CstDate { get; set; }
        [StringLength(20)]
        public string LstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LstDate { get; set; }
        [StringLength(20)]
        public string VatNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? VatDate { get; set; }
        [StringLength(25)]
        public string ExciseRange { get; set; }
        [StringLength(25)]
        public string ExciseDivn { get; set; }
        [StringLength(10)]
        public string DivCode { get; set; }
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(10)]
        public string Currency { get; set; }
        [StringLength(10)]
        public string GcaCode { get; set; }
        [StringLength(3)]
        public string InsureId { get; set; }
        [Column(TypeName = "int(11)")]
        public int? Mktgmage { get; set; }
        [StringLength(6)]
        public string OfficeId { get; set; }
        [Column(TypeName = "float(5,2)")]
        public float? Cessper { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string Connectivity { get; set; }
        [Column("GetWayIP")]
        [StringLength(15)]
        public string GetWayIp { get; set; }
        [Column("StartIP")]
        [StringLength(15)]
        public string StartIp { get; set; }
        [Column("EndIP")]
        [StringLength(15)]
        public string EndIp { get; set; }
        [StringLength(15)]
        public string SubnetMask { get; set; }
        [StringLength(15)]
        public string DailUpNo { get; set; }
        [StringLength(50)]
        public string SerProvider { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ConntyFrom { get; set; }
        [Column(TypeName = "int(3)")]
        public int? NoOfComp { get; set; }
        [StringLength(10)]
        public string BrInCharge { get; set; }
        [StringLength(10)]
        public string BrAcUser { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(12)]
        public string BeAcOnCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AcStDate { get; set; }
        [StringLength(10)]
        public string AcCode { get; set; }
        [StringLength(7)]
        public string NwmzPostdt { get; set; }
        [StringLength(7)]
        public string NwmzPoEndDt { get; set; }
        [StringLength(7)]
        public string NwmzPostDt1 { get; set; }
        [StringLength(7)]
        public string NwmzPoEndDt1 { get; set; }
        [StringLength(3)]
        public string OpTransSys { get; set; }
        [StringLength(4)]
        public string OrdNo { get; set; }
        [StringLength(25)]
        public string PrimaryDns { get; set; }
        [StringLength(25)]
        public string AlternateDns { get; set; }
        [StringLength(3)]
        public string Esicoverage { get; set; }
        [Column("PTaxCoverage")]
        [StringLength(3)]
        public string PtaxCoverage { get; set; }
        [StringLength(10)]
        public string ParBrCode { get; set; }
        [StringLength(20)]
        public string TinNo { get; set; }
        [StringLength(20)]
        public string PanNo { get; set; }

        [InverseProperty("BranchCodeNavigation")]
        public virtual ICollection<TmAcbrmap> TmAcbrmap { get; set; }
        [InverseProperty(nameof(TmEmployee.BranchCodeNavigation))]
        public virtual ICollection<TmEmployee> TmEmployeeBranchCodeNavigation { get; set; }
        [InverseProperty(nameof(TmEmployee.SubBrCodeNavigation))]
        public virtual ICollection<TmEmployee> TmEmployeeSubBrCodeNavigation { get; set; }
        [InverseProperty("RobrCodeNavigation")]
        public virtual ICollection<TmRegion> TmRegion { get; set; }
        [InverseProperty("BranchCodeNavigation")]
        public virtual ICollection<TmUserdefault> TmUserdefault { get; set; }
        [InverseProperty("BranchCodeNavigation")]
        public virtual ICollection<TmUserright> TmUserright { get; set; }
        [InverseProperty("ZoneBrCodeNavigation")]
        public virtual ICollection<TmZone> TmZone { get; set; }
    }
}
