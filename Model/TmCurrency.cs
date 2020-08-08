using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_currency")]
    public partial class TmCurrency
    {
        public TmCurrency()
        {
            TmPlace = new HashSet<TmPlace>();
        }

        [Key]
        [StringLength(10)]
        public string Currency { get; set; }
        [Required]
        [StringLength(35)]
        public string CurrencyDesc { get; set; }
        [Required]
        [StringLength(6)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [InverseProperty("CurrencyNavigation")]
        public virtual ICollection<TmPlace> TmPlace { get; set; }
    }
}
