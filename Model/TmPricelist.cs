using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_pricelist")]
    public partial class TmPricelist
    {
        [Required]
        [StringLength(12)]
        public string DocNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime TransDate { get; set; }
        [Required]
        [StringLength(2)]
        public string PricingType { get; set; }
        [Column(TypeName = "date")]
        public DateTime EffectFrom { get; set; }
        [Column(TypeName = "date")]
        public DateTime EffectTo { get; set; }
        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [StringLength(10)]
        public string ItemCode { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DiscPer { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Column(TypeName = "int(2)")]
        public int AcYearNo { get; set; }
        [Key]
        [Column(TypeName = "int(15)")]
        public int DocIntNo { get; set; }
        [Key]
        [StringLength(10)]
        public string PartyCode { get; set; }
        [Key]
        [StringLength(4)]
        public string Age { get; set; }
        [Key]
        [StringLength(10)]
        public string ItemNature { get; set; }
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [StringLength(10)]
        public string DivCode { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(10)]
        public string SubBrCode { get; set; }
        [StringLength(10)]
        public string LogBrCode { get; set; }
        [StringLength(10)]
        public string RegCode { get; set; }
        [StringLength(10)]
        public string RegName { get; set; }
    }
}
