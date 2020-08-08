using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_analysis")]
    public partial class TmAnalysis
    {
        public TmAnalysis()
        {
            TmAccountsAnalCode1Navigation = new HashSet<TmAccounts>();
            TmAccountsAnalCode2Navigation = new HashSet<TmAccounts>();
            TmAccountsAnalCode3Navigation = new HashSet<TmAccounts>();
            TmAccountsAnalCode4Navigation = new HashSet<TmAccounts>();
            TmAccountsAnalCode5Navigation = new HashSet<TmAccounts>();
        }

        [Key]
        [StringLength(10)]
        public string AnalCode { get; set; }
        [Required]
        [StringLength(25)]
        public string AnalName { get; set; }
        [Required]
        [StringLength(3)]
        public string Type { get; set; }
        [StringLength(1)]
        public string Category { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty(nameof(TmAccounts.AnalCode1Navigation))]
        public virtual ICollection<TmAccounts> TmAccountsAnalCode1Navigation { get; set; }
        [InverseProperty(nameof(TmAccounts.AnalCode2Navigation))]
        public virtual ICollection<TmAccounts> TmAccountsAnalCode2Navigation { get; set; }
        [InverseProperty(nameof(TmAccounts.AnalCode3Navigation))]
        public virtual ICollection<TmAccounts> TmAccountsAnalCode3Navigation { get; set; }
        [InverseProperty(nameof(TmAccounts.AnalCode4Navigation))]
        public virtual ICollection<TmAccounts> TmAccountsAnalCode4Navigation { get; set; }
        [InverseProperty(nameof(TmAccounts.AnalCode5Navigation))]
        public virtual ICollection<TmAccounts> TmAccountsAnalCode5Navigation { get; set; }
    }
}
