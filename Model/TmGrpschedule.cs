using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_grpschedule")]
    public partial class TmGrpschedule
    {
        public TmGrpschedule()
        {
            InversePrtSchNoNavigation = new HashSet<TmGrpschedule>();
            TmAcgroup = new HashSet<TmAcgroup>();
        }

        [Key]
        [Column(TypeName = "int(3)")]
        public int SchNo { get; set; }
        [Required]
        [StringLength(35)]
        public string SchName { get; set; }
        [Column(TypeName = "int(3)")]
        public int PrtSchNo { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(PrtSchNo))]
        [InverseProperty(nameof(TmGrpschedule.InversePrtSchNoNavigation))]
        public virtual TmGrpschedule PrtSchNoNavigation { get; set; }
        [InverseProperty(nameof(TmGrpschedule.PrtSchNoNavigation))]
        public virtual ICollection<TmGrpschedule> InversePrtSchNoNavigation { get; set; }
        [InverseProperty("SchNoNavigation")]
        public virtual ICollection<TmAcgroup> TmAcgroup { get; set; }
    }
}
