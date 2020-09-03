using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_itembrmap")]
    public partial class TmItembrmap
    {
        public TmItembrmap()
        {
            TmPricelist = new HashSet<TmPricelist>();
        }

        [Key]
        [StringLength(10)]
        public string ItemCode { get; set; }
        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Column(TypeName = "int(10)")]
        public int MinLvl { get; set; }
        [Column(TypeName = "int(10)")]
        public int MaxLvl { get; set; }
        [Column(TypeName = "int(10)")]
        public int ReOrdLvl { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal MinRate { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal MaxRate { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal AvgRate { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal? LastPurRate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastPurDate { get; set; }
        [Required]
        [StringLength(12)]
        public string LastSupplier { get; set; }
        public DateTimeOffset LockDate { get; set; }
        [Column(TypeName = "int(2)")]
        public int LeadTime { get; set; }
        [Required]
        [StringLength(1)]
        public string IsTaxable { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? TaxRate { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastIssuseDate { get; set; }

        [ForeignKey(nameof(ItemCode))]
        [InverseProperty(nameof(TmItem.TmItembrmap))]
        public virtual TmItem ItemCodeNavigation { get; set; }
        [InverseProperty("TmItembrmap")]
        public virtual ICollection<TmPricelist> TmPricelist { get; set; }
    }
}
