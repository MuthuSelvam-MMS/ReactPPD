using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_desgn")]
    public partial class TmDesgn
    {
        public TmDesgn()
        {
            TmEmployeeAdmRepHeadNavigation = new HashSet<TmEmployee>();
            TmEmployeeJoinAsNavigation = new HashSet<TmEmployee>();
            TmEmployeeSubstituteNavigation = new HashSet<TmEmployee>();
        }

        [Key]
        [StringLength(10)]
        public string DesgnCode { get; set; }
        [Required]
        [StringLength(35)]
        public string DesgName { get; set; }
        [StringLength(10)]
        public string DesgnGrpCode { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty(nameof(TmEmployee.AdmRepHeadNavigation))]
        public virtual ICollection<TmEmployee> TmEmployeeAdmRepHeadNavigation { get; set; }
        [InverseProperty(nameof(TmEmployee.JoinAsNavigation))]
        public virtual ICollection<TmEmployee> TmEmployeeJoinAsNavigation { get; set; }
        [InverseProperty(nameof(TmEmployee.SubstituteNavigation))]
        public virtual ICollection<TmEmployee> TmEmployeeSubstituteNavigation { get; set; }
    }
}
