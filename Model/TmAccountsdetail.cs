using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_accountsdetail")]
    public partial class TmAccountsdetail
    {
        public TmAccountsdetail()
        {
            TmAcbrmap = new HashSet<TmAcbrmap>();
            TmDoctypesAcCode11 = new HashSet<TmDoctypes>();
            TmDoctypesAcCode2 = new HashSet<TmDoctypes>();
            TmVendor = new HashSet<TmVendor>();
        }

        [Key]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [StringLength(40)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        [StringLength(40)]
        public string Address3 { get; set; }
        [StringLength(35)]
        public string LandMark { get; set; }
        [StringLength(10)]
        public string PinCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }
        [Required]
        [StringLength(25)]
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
        [StringLength(25)]
        public string PhoneNo { get; set; }
        [StringLength(25)]
        public string FaxNo { get; set; }
        [StringLength(15)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string MailId { get; set; }
        [StringLength(25)]
        public string Website { get; set; }
        [StringLength(35)]
        public string ContactPer { get; set; }
        [StringLength(16)]
        public string AcRefNo { get; set; }
        [StringLength(25)]
        public string CstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CstDate { get; set; }
        [StringLength(25)]
        public string LstNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LstDate { get; set; }
        [StringLength(25)]
        public string VatNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? VatDate { get; set; }
        [StringLength(25)]
        public string PanNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PanDate { get; set; }
        [Required]
        [StringLength(10)]
        public string BankCode { get; set; }
        [StringLength(35)]
        public string BankAdd1 { get; set; }
        [StringLength(35)]
        public string BankAdd2 { get; set; }
        [StringLength(35)]
        public string BankAdd3 { get; set; }
        [Required]
        [StringLength(10)]
        public string PayableAt { get; set; }
        [StringLength(55)]
        public string PayFavour { get; set; }
        [StringLength(60)]
        public string Owner { get; set; }
        [Column(TypeName = "int(10)")]
        public int? AddRid { get; set; }
        [StringLength(25)]
        public string PdcNo { get; set; }
        [StringLength(25)]
        public string PdcRefNo { get; set; }
        [StringLength(25)]
        public string PdcAppNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? GstDate { get; set; }
        [StringLength(25)]
        public string GstNo { get; set; }
        [StringLength(50)]
        public string FavourOf { get; set; }

        [ForeignKey(nameof(AccountCode))]
        [InverseProperty(nameof(TmAccounts.TmAccountsdetail))]
        public virtual TmAccounts AccountCodeNavigation { get; set; }
        [InverseProperty("AccountCode1")]
        public virtual ICollection<TmAcbrmap> TmAcbrmap { get; set; }
        [InverseProperty(nameof(TmDoctypes.AcCode11))]
        public virtual ICollection<TmDoctypes> TmDoctypesAcCode11 { get; set; }
        [InverseProperty(nameof(TmDoctypes.AcCode2))]
        public virtual ICollection<TmDoctypes> TmDoctypesAcCode2 { get; set; }
        [InverseProperty("AccountCode1")]
        public virtual ICollection<TmVendor> TmVendor { get; set; }
    }
}
