using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_acyearset")]
    public partial class TmAcyearset
    {
        public TmAcyearset()
        {
            TmDoctypes = new HashSet<TmDoctypes>();
        }

        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [Column(TypeName = "int(11)")]
        public int AcYearNo { get; set; }
        [Column("FromMM", TypeName = "int(11)")]
        public int FromMm { get; set; }
        [Column("FromYYYY", TypeName = "int(11)")]
        public int FromYyyy { get; set; }
        [Column("ToMM", TypeName = "int(11)")]
        public int ToMm { get; set; }
        [Column("ToYYYY", TypeName = "int(11)")]
        public int? ToYyyy { get; set; }
        [Column(TypeName = "decimal(20,0)")]
        public decimal VouintlNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime LockDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? AcStartDate { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? InvLockDate { get; set; }
        [StringLength(1)]
        public string OpstkTrans { get; set; }
        [StringLength(1)]
        public string EntryAllow { get; set; }
        [StringLength(1)]
        public string OpBalAllocTran { get; set; }
        [StringLength(1)]
        public string AllocTryy { get; set; }

        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmAcyearset))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
        [InverseProperty("TmAcyearset")]
        public virtual ICollection<TmDoctypes> TmDoctypes { get; set; }
    }
}
