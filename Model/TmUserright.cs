using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_userright")]
    public partial class TmUserright
    {
        [Key]
        [StringLength(10)]
        public string UserId { get; set; }
        [StringLength(10)]
        public string UnitType { get; set; }
        [Key]
        [StringLength(5)]
        public string BranchCode { get; set; }
        [Key]
        [StringLength(5)]
        public string MenuId { get; set; }
        [Column("USave")]
        [StringLength(1)]
        public string Usave { get; set; }
        [Column("UEdit")]
        [StringLength(1)]
        public string Uedit { get; set; }
        [Column("UView")]
        [StringLength(1)]
        public string Uview { get; set; }
        [Column("UPrint")]
        [StringLength(1)]
        public string Uprint { get; set; }
        [Column("UDelete")]
        [StringLength(1)]
        public string Udelete { get; set; }
        [Column("UWorkFlow")]
        [StringLength(1)]
        public string UworkFlow { get; set; }
        [StringLength(1)]
        public string RowFor { get; set; }

        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmUserright))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
        [ForeignKey(nameof(UnitType))]
        [InverseProperty(nameof(TmDivision.TmUserright))]
        public virtual TmDivision UnitTypeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmEmployee.TmUserright))]
        public virtual TmEmployee User { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmUser.TmUserright))]
        public virtual TmUser UserNavigation { get; set; }
    }
}
