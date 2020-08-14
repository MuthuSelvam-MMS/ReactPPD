using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_menuitem")]
    public partial class TmMenuitem
    {
        [Key]
        [StringLength(10)]
        public string MenuId { get; set; }
        [Required]
        [StringLength(1)]
        public string MenuType { get; set; }
        [Required]
        [StringLength(35)]
        public string MenuCaption { get; set; }
        [StringLength(100)]
        public string MenuUrlL { get; set; }
        [StringLength(10)]
        public string ParentId { get; set; }
        [Required]
        [StringLength(10)]
        public string GroupId { get; set; }
        [Column(TypeName = "int(6)")]
        public int PosVertical { get; set; }
        [Column(TypeName = "int(6)")]
        public int PosHoriz { get; set; }
        [Column(TypeName = "int(6)")]
        public int? MenuLevel { get; set; }
        [Required]
        [StringLength(10)]
        public string UnitType { get; set; }
        [StringLength(100)]
        public string TableName { get; set; }
        [StringLength(100)]
        public string ColumnName { get; set; }
        [StringLength(5)]
        public string DocPrefix { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(6)]
        public string AllocTrType { get; set; }
        [StringLength(1)]
        public string WorkFlow { get; set; }
        [StringLength(10)]
        public string WfDocType { get; set; }
        [StringLength(35)]
        public string WfCaption { get; set; }
        [StringLength(100)]
        public string WfUrl { get; set; }
        [StringLength(100)]
        public string ActionForm { get; set; }
        [StringLength(10)]
        public string RptOption { get; set; }
        [StringLength(3)]
        public string ModuleCode { get; set; }
        [StringLength(10)]
        public string FormId { get; set; }
        [StringLength(10)]
        public string DelStatus { get; set; }
        [StringLength(1)]
        public string DelAllow { get; set; }
        [StringLength(1)]
        public string StkChk { get; set; }
        [StringLength(1)]
        public string AccChk { get; set; }
        [StringLength(1)]
        public string CondRowBuild { get; set; }
        [StringLength(30)]
        public string DocVal { get; set; }
        [StringLength(1)]
        public string PpSize { get; set; }
        [StringLength(1)]
        public string RoDocPrefix { get; set; }
        [StringLength(3)]
        public string PrintNotAllow { get; set; }
        [StringLength(10)]
        public string Div1 { get; set; }
        [StringLength(1)]
        public string EditAllow { get; set; }
        [StringLength(10)]
        public string EditNotAllow { get; set; }
        [StringLength(1)]
        public string DocTypeCheck { get; set; }
        [StringLength(30)]
        public string AcItemType { get; set; }
        [StringLength(1)]
        public string AcPartYgc { get; set; }
        [StringLength(30)]
        public string StateCheck { get; set; }
        [StringLength(10)]
        public string ItemType { get; set; }
        [StringLength(30)]
        public string ExpCodeTable { get; set; }
        [StringLength(1)]
        public string TransferId { get; set; }
        [StringLength(30)]
        public string SaveConfirm { get; set; }
        [Column(TypeName = "int(6)")]
        public int? ReportCpi { get; set; }
        [StringLength(10)]
        public string MisLevels { get; set; }
        [StringLength(1)]
        public string CashSplit { get; set; }
        [Column(TypeName = "smallint(6)")]
        public short? Ver { get; set; }
        [StringLength(10)]
        public string BcondType { get; set; }
        [Column("MSelect")]
        [StringLength(6)]
        public string Mselect { get; set; }
        [StringLength(1)]
        public string ImpRep { get; set; }
        [StringLength(1)]
        public string ModuleType { get; set; }
        [StringLength(1)]
        public string IsExp { get; set; }
        [StringLength(5)]
        public string OldPrefix { get; set; }
        [StringLength(1)]
        public string IsRoBr { get; set; }
        [StringLength(1)]
        public string HstFrom { get; set; }
    }
}
