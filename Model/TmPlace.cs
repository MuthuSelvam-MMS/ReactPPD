using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_place")]
    public partial class TmPlace
    {
        public TmPlace()
        {
            InverseCityCodeNavigation = new HashSet<TmPlace>();
            InverseCountryCodeNavigation = new HashSet<TmPlace>();
            InverseDistCodeNavigation = new HashSet<TmPlace>();
            InversePinCodeNavigation = new HashSet<TmPlace>();
            InverseStateCodeNavigation = new HashSet<TmPlace>();
            InverseTalukCodeNavigation = new HashSet<TmPlace>();
        }

        [Required]
        [StringLength(1)]
        public string PlaceType { get; set; }
        [Key]
        [StringLength(10)]
        public string PlaceCode { get; set; }
        [Required]
        [StringLength(40)]
        public string PlaceName { get; set; }
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(40)]
        public string CountryName { get; set; }
        [Required]
        [StringLength(10)]
        public string StateCode { get; set; }
        [StringLength(40)]
        public string StateName { get; set; }
        [Required]
        [StringLength(10)]
        public string DistCode { get; set; }
        [StringLength(40)]
        public string DistName { get; set; }
        [Required]
        [StringLength(10)]
        public string TalukCode { get; set; }
        [StringLength(40)]
        public string TalukName { get; set; }
        [Required]
        [StringLength(10)]
        public string CityCode { get; set; }
        [StringLength(40)]
        public string CityName { get; set; }
        [Required]
        [StringLength(10)]
        public string PinCode { get; set; }
        [StringLength(40)]
        public string PostOff { get; set; }
        [Column(TypeName = "int(5)")]
        public int? SlNo { get; set; }
        [StringLength(10)]
        public string Currency { get; set; }
        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string StdCode { get; set; }

        [ForeignKey(nameof(CityCode))]
        [InverseProperty(nameof(TmPlace.InverseCityCodeNavigation))]
        public virtual TmPlace CityCodeNavigation { get; set; }
        [ForeignKey(nameof(CountryCode))]
        [InverseProperty(nameof(TmPlace.InverseCountryCodeNavigation))]
        public virtual TmPlace CountryCodeNavigation { get; set; }
        [ForeignKey(nameof(Currency))]
        [InverseProperty(nameof(TmCurrency.TmPlace))]
        public virtual TmCurrency CurrencyNavigation { get; set; }
        [ForeignKey(nameof(DistCode))]
        [InverseProperty(nameof(TmPlace.InverseDistCodeNavigation))]
        public virtual TmPlace DistCodeNavigation { get; set; }
        [ForeignKey(nameof(PinCode))]
        [InverseProperty(nameof(TmPlace.InversePinCodeNavigation))]
        public virtual TmPlace PinCodeNavigation { get; set; }
        [ForeignKey(nameof(StateCode))]
        [InverseProperty(nameof(TmPlace.InverseStateCodeNavigation))]
        public virtual TmPlace StateCodeNavigation { get; set; }
        [ForeignKey(nameof(TalukCode))]
        [InverseProperty(nameof(TmPlace.InverseTalukCodeNavigation))]
        public virtual TmPlace TalukCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.CityCodeNavigation))]
        public virtual ICollection<TmPlace> InverseCityCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.CountryCodeNavigation))]
        public virtual ICollection<TmPlace> InverseCountryCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.DistCodeNavigation))]
        public virtual ICollection<TmPlace> InverseDistCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.PinCodeNavigation))]
        public virtual ICollection<TmPlace> InversePinCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.StateCodeNavigation))]
        public virtual ICollection<TmPlace> InverseStateCodeNavigation { get; set; }
        [InverseProperty(nameof(TmPlace.TalukCodeNavigation))]
        public virtual ICollection<TmPlace> InverseTalukCodeNavigation { get; set; }
    }
}
