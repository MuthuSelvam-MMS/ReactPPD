using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_userdivmap")]
    public partial class TmUserdivmap
    {
        [Key]
        [StringLength(10)]
        public string UserId { get; set; }
        [Key]
        [StringLength(10)]
        public string DivCode { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }

        [ForeignKey(nameof(DivCode))]
        [InverseProperty(nameof(TmDivision.TmUserdivmap))]
        public virtual TmDivision DivCodeNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmEmployee.TmUserdivmap))]
        public virtual TmEmployee User { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TmUser.TmUserdivmap))]
        public virtual TmUser UserNavigation { get; set; }
    }
}
