using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_company")]
    public partial class TmCompany
    {
        [Key]
        [StringLength(10)]
        public string CompanyCodeVarchar { get; set; }
        [StringLength(60)]
        public string CompanyName { get; set; }
        [StringLength(7)]
        public string ShortName { get; set; }
        [StringLength(12)]
        public string GcacCode { get; set; }
        [StringLength(35)]
        public string AdminOffAdd1 { get; set; }
        [StringLength(35)]
        public string AdminOffAdd2 { get; set; }
        [StringLength(35)]
        public string AdminOffAdd3 { get; set; }
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
    }
}
