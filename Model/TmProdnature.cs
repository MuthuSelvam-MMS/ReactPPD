using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_prodnature")]
    public partial class TmProdnature
    {
        public TmProdnature()
        {
            TmItem = new HashSet<TmItem>();
        }

        [Key]
        [StringLength(2)]
        public string Code { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("ProdNatureNavigation")]
        public virtual ICollection<TmItem> TmItem { get; set; }
    }
}
