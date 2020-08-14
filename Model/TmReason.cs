using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_reason")]
    public partial class TmReason
    {
        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [StringLength(10)]
        public string ReasonCode { get; set; }
        [Required]
        [StringLength(30)]
        public string ReasonName { get; set; }
        [Key]
        [StringLength(5)]
        public string DocType { get; set; }
        [Required]
        [StringLength(10)]
        public string GrpCode { get; set; }
        [Required]
        [StringLength(12)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(12)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmReason))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
    }
}
