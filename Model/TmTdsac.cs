using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_tdsac")]
    public partial class TmTdsac
    {
        [Key]
        [StringLength(10)]
        public string Nature { get; set; }
        [Key]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal? DedPer { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        [Key]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(15)]
        public string DocNo { get; set; }
        [Key]
        [Column(TypeName = "int(15)")]
        public int DocIntNo { get; set; }

        [ForeignKey(nameof(Nature))]
        [InverseProperty(nameof(TmTdsnature.TmTdsac))]
        public virtual TmTdsnature NatureNavigation { get; set; }
    }
}
