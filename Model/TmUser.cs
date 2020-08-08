using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_user")]
    public partial class TmUser
    {
        public TmUser()
        {
            TmUserright = new HashSet<TmUserright>();
        }

        [Key]
        [Column("UserID")]
        [StringLength(10)]
        public string UserId { get; set; }
        [StringLength(15)]
        public string PassWord { get; set; }
        [StringLength(10)]
        public string Category { get; set; }
        [StringLength(50)]
        public string MailId { get; set; }
        [StringLength(10)]
        public string ReigionCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PasswordChangeDate { get; set; }
        [StringLength(3)]
        public string PassWordCheckMode { get; set; }
        [Column(TypeName = "int(3)")]
        public int? PassWordValidIpTo { get; set; }
        [StringLength(3)]
        public string AdminUser { get; set; }
        [StringLength(3)]
        public string Logged { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LoggedDate { get; set; }
        [StringLength(30)]
        public string SessionId { get; set; }
        [StringLength(10)]
        public string DocType { get; set; }
        [StringLength(10)]
        public string InteCode { get; set; }
        [StringLength(3)]
        public string EliteUser { get; set; }
        [StringLength(3)]
        public string AddRights { get; set; }
        [StringLength(3)]
        public string MisRights { get; set; }

        [ForeignKey(nameof(ReigionCode))]
        [InverseProperty(nameof(TmRegion.TmUser))]
        public virtual TmRegion ReigionCodeNavigation { get; set; }
        [InverseProperty("UserNavigation")]
        public virtual TmUserdefault TmUserdefault { get; set; }
        [InverseProperty("UserNavigation")]
        public virtual ICollection<TmUserright> TmUserright { get; set; }
    }
}
