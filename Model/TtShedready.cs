using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tt_shedready")]
    public partial class TtShedready
    {
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [StringLength(10)]
        public string ZoneCode { get; set; }
        [StringLength(10)]
        public string BranchCode { get; set; }
        [StringLength(10)]
        public string SubBrCode { get; set; }
        [StringLength(10)]
        public string LineCode { get; set; }
        [Column(TypeName = "int(2)")]
        public int? AcYearNo { get; set; }
        [Required]
        [StringLength(15)]
        public string DocNo { get; set; }
        [Column(TypeName = "int(25)")]
        public int? TransNo { get; set; }
        public DateTime? DocDate { get; set; }
        [StringLength(10)]
        public string RefNo { get; set; }
        public DateTime? RefDate { get; set; }
        [StringLength(12)]
        public string Supervisor { get; set; }
        [StringLength(12)]
        public string FarmCode { get; set; }
        [Column(TypeName = "int(4)")]
        public int? BatchNo { get; set; }
        [Column(TypeName = "int(5)")]
        public int? Capacity { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? BirdPerSqft { get; set; }
        [Column(TypeName = "int(6)")]
        public int? BirdsCapacity { get; set; }
        [StringLength(1)]
        public string Status { get; set; }
        [StringLength(1)]
        public string SopStatus { get; set; }
        [Column(TypeName = "int(6)")]
        public int? ScheduleQty { get; set; }
        [Column(TypeName = "int(6)")]
        public int? OrderQty { get; set; }
        [Column(TypeName = "int(10)")]
        public int? DcQty { get; set; }
        [StringLength(10)]
        public string RegZoneCode { get; set; }
        [Key]
        [Column(TypeName = "int(15)")]
        public int DocIntNo { get; set; }
        [Column(TypeName = "int(15)")]
        public int? MaleRatio { get; set; }
    }
}
