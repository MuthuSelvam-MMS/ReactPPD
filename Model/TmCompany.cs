using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_company")]
    public partial class TmCompany
    {
        public TmCompany()
        {
            TmDoctypes = new HashSet<TmDoctypes>();
            TmRegionmap = new HashSet<TmRegionmap>();
            TmRegzonemap = new HashSet<TmRegzonemap>();
        }

        [Key]
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [Required]
        [StringLength(60)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(7)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(12)]
        public string GcacCode { get; set; }
        [Required]
        [StringLength(35)]
        public string AdminOffAdd1 { get; set; }
        [Required]
        [StringLength(35)]
        public string AdminOffAdd2 { get; set; }
        [Required]
        [StringLength(35)]
        public string AdminOffAdd3 { get; set; }
        [Required]
        [StringLength(10)]
        public string PinCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }
        [Required]
        [StringLength(10)]
        public string TalukCode { get; set; }
        [Required]
        [StringLength(10)]
        public string DistCode { get; set; }
        [Required]
        [StringLength(10)]
        public string StateCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }
        [StringLength(30)]
        public string MailId { get; set; }
        [StringLength(15)]
        public string PhoneNo { get; set; }
        [StringLength(15)]
        public string FaxNo { get; set; }
        [StringLength(15)]
        public string MobileNo { get; set; }
        [StringLength(15)]
        public string CstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CstDate { get; set; }
        [StringLength(15)]
        public string LstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LstDate { get; set; }
        [StringLength(20)]
        public string VatNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? VatDate { get; set; }
        [StringLength(20)]
        public string DlNo { get; set; }
        [StringLength(20)]
        public string ItCircle { get; set; }
        [StringLength(20)]
        public string CeNo { get; set; }
        [StringLength(20)]
        public string PanNo { get; set; }
        [StringLength(50)]
        public string WebSite { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage1 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage2 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage3 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage4 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage5 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Rage6 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page1 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page2 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page3 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page4 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page5 { get; set; }
        [Column(TypeName = "int(5)")]
        public int Page6 { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(GcacCode))]
        [InverseProperty(nameof(TmAccounts.TmCompany))]
        public virtual TmAccounts GcacCodeNavigation { get; set; }
        [InverseProperty("CompanyCodeNavigation")]
        public virtual ICollection<TmDoctypes> TmDoctypes { get; set; }
        [InverseProperty("CompanyCodeNavigation")]
        public virtual ICollection<TmRegionmap> TmRegionmap { get; set; }
        [InverseProperty("CompanyCodeNavigation")]
        public virtual ICollection<TmRegzonemap> TmRegzonemap { get; set; }
    }
}
