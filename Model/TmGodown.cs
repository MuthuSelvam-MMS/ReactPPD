using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_godown")]
    public partial class TmGodown
    {
        [Required]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [StringLength(12)]
        public string GoDownCode { get; set; }
        [StringLength(40)]
        public string GoDownName { get; set; }
        [Required]
        [StringLength(2)]
        public string GoDownType { get; set; }
        [Required]
        [StringLength(2)]
        public string WeighBridge { get; set; }
        [Required]
        [StringLength(10)]
        public string CcCode { get; set; }
        [StringLength(60)]
        public string Address1 { get; set; }
        [StringLength(60)]
        public string Address2 { get; set; }
        [StringLength(60)]
        public string Address3 { get; set; }
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
        [StringLength(15)]
        public string PhoneNo { get; set; }
        [StringLength(15)]
        public string FaxNo { get; set; }
        [StringLength(15)]
        public string MobileNo { get; set; }
        [StringLength(20)]
        public string MailId { get; set; }
        [StringLength(25)]
        public string ContactPer { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
    }
}
