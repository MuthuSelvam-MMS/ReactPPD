using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_uom")]
    public partial class TmUom
    {
        public TmUom()
        {
            TmItemUomBigNavigation = new HashSet<TmItem>();
            TmItemUomPurNavigation = new HashSet<TmItem>();
            TmItemUomSmallNavigation = new HashSet<TmItem>();
            TmItemUomStkNavigation = new HashSet<TmItem>();
        }

        [Key]
        [StringLength(5)]
        public string Uom { get; set; }
        [StringLength(30)]
        public string UomName { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty(nameof(TmItem.UomBigNavigation))]
        public virtual ICollection<TmItem> TmItemUomBigNavigation { get; set; }
        [InverseProperty(nameof(TmItem.UomPurNavigation))]
        public virtual ICollection<TmItem> TmItemUomPurNavigation { get; set; }
        [InverseProperty(nameof(TmItem.UomSmallNavigation))]
        public virtual ICollection<TmItem> TmItemUomSmallNavigation { get; set; }
        [InverseProperty(nameof(TmItem.UomStkNavigation))]
        public virtual ICollection<TmItem> TmItemUomStkNavigation { get; set; }
    }
}
