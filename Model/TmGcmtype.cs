using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_gcmtype")]
    public partial class TmGcmtype
    {
        public TmGcmtype()
        {
            TmGcm = new HashSet<TmGcm>();
        }

        [Key]
        [StringLength(10)]
        public string GcmType { get; set; }
        [StringLength(40)]
        public string Descn { get; set; }
        [Required]
        [StringLength(10)]
        public string Usage { get; set; }
        [StringLength(5)]
        public string EnteredBy { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
        [Column(TypeName = "int(2)")]
        public int? CodeSize { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("GcmTypeNavigation")]
        public virtual ICollection<TmGcm> TmGcm { get; set; }
    }
}
