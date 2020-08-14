using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_docclass")]
    public partial class TmDocclass
    {
        public TmDocclass()
        {
            TmDoctypes = new HashSet<TmDoctypes>();
        }

        [Key]
        [StringLength(25)]
        public string DocClass { get; set; }
        [Required]
        [StringLength(50)]
        public string DocClName { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("DocClassNavigation")]
        public virtual ICollection<TmDoctypes> TmDoctypes { get; set; }
    }
}
