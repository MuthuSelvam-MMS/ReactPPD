using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_gcm")]
    public partial class TmGcm
    {
        [Required]
        [StringLength(10)]
        public string GcmType { get; set; }
        [Key]
        [StringLength(15)]
        public string GcmCode { get; set; }
        [Required]
        [StringLength(40)]
        public string GcmDesc { get; set; }
        [Column(TypeName = "int(5)")]
        public int SlNo { get; set; }
        [StringLength(50)]
        public string Remarks { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string Grp { get; set; }

        [ForeignKey(nameof(GcmType))]
        [InverseProperty(nameof(TmGcmtype.TmGcm))]
        public virtual TmGcmtype GcmTypeNavigation { get; set; }
    }
}
