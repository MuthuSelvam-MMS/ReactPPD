using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_meats")]
    public partial class TmMeats
    {
        [Key]
        [StringLength(10)]
        public string MeatsCode { get; set; }
        [Required]
        [StringLength(50)]
        public string MeatsName { get; set; }
        [Required]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Column(TypeName = "int(10)")]
        public int AcYearNo { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [StringLength(5)]
        public string Uom { get; set; }
        [StringLength(10)]
        public string Section { get; set; }
        [StringLength(1)]
        public string GradeSection { get; set; }
        [StringLength(20)]
        public string SectionName { get; set; }
    }
}
