using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_doctables")]
    public partial class TmDoctables
    {
        [Key]
        [StringLength(10)]
        public string DocType { get; set; }
        [Key]
        [StringLength(50)]
        public string TableName { get; set; }
        [Key]
        [StringLength(100)]
        public string ProcedureName { get; set; }
        [Key]
        [Column(TypeName = "int(2)")]
        public int OrdId { get; set; }
        [Key]
        [StringLength(3)]
        public string Operation { get; set; }
        [Column("MRows")]
        [StringLength(1)]
        public string Mrows { get; set; }
        [StringLength(1)]
        public string UpdStatus { get; set; }
        [Column(TypeName = "int(2)")]
        public int? SgOrdId { get; set; }
        [Column(TypeName = "int(2)")]
        public int? PrtOrdId { get; set; }
        [StringLength(2)]
        public string GridColTitle { get; set; }
        [StringLength(50)]
        public string ExCpmSg { get; set; }
        [StringLength(1)]
        public string AddParam { get; set; }
        [StringLength(1)]
        public string RowNo { get; set; }
        [StringLength(1)]
        public string Header { get; set; }
    }
}
