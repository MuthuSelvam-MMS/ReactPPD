using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_itemnature")]
    public partial class TmItemnature
    {
        [Key]
        [StringLength(10)]
        public string ItemCode { get; set; }
        [Key]
        [StringLength(6)]
        public string Nature { get; set; }
        [StringLength(35)]
        public string Descn { get; set; }
        [StringLength(10)]
        public string ContCode { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(ItemCode))]
        [InverseProperty(nameof(TmItem.TmItemnature))]
        public virtual TmItem ItemCodeNavigation { get; set; }
    }
}
