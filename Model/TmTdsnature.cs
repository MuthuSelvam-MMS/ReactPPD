using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_tdsnature")]
    public partial class TmTdsnature
    {
        public TmTdsnature()
        {
            TmTdsac = new HashSet<TmTdsac>();
        }

        [Key]
        [StringLength(10)]
        public string NatureCode { get; set; }
        [StringLength(25)]
        public string NatureDesc { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("NatureNavigation")]
        public virtual ICollection<TmTdsac> TmTdsac { get; set; }
    }
}
