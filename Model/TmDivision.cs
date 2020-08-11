using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_division")]
    public partial class TmDivision
    {
        public TmDivision()
        {
            InversePrtDivCodeNavigation = new HashSet<TmDivision>();
            TmRegionmap = new HashSet<TmRegionmap>();
            TmRegzonemap = new HashSet<TmRegzonemap>();
            TmUserdefault = new HashSet<TmUserdefault>();
            TmUserright = new HashSet<TmUserright>();
        }

        [Key]
        [StringLength(10)]
        public string DivCode { get; set; }
        [StringLength(30)]
        public string DivName { get; set; }
        [StringLength(6)]
        public string ShortName { get; set; }
        [Column(TypeName = "int(5)")]
        public int? SlNo { get; set; }
        [StringLength(3)]
        public string IsActive { get; set; }
        [StringLength(10)]
        public string PrtDivCode { get; set; }
        [StringLength(3)]
        public string FeedReq { get; set; }
        [StringLength(10)]
        public string ShortCode { get; set; }
        [Column(TypeName = "int(3)")]
        public int? OrdNo { get; set; }
        [StringLength(15)]
        public string Div { get; set; }
        [Column("BConIdsFix")]
        [StringLength(10)]
        public string BconIdsFix { get; set; }
        [Column("BConDiv")]
        [StringLength(10)]
        public string BconDiv { get; set; }
        [Column("BConBusArea")]
        [StringLength(10)]
        public string BconBusArea { get; set; }

        [ForeignKey(nameof(PrtDivCode))]
        [InverseProperty(nameof(TmDivision.InversePrtDivCodeNavigation))]
        public virtual TmDivision PrtDivCodeNavigation { get; set; }
        [InverseProperty(nameof(TmDivision.PrtDivCodeNavigation))]
        public virtual ICollection<TmDivision> InversePrtDivCodeNavigation { get; set; }
        [InverseProperty("DivCodeNavigation")]
        public virtual ICollection<TmRegionmap> TmRegionmap { get; set; }
        [InverseProperty("DivCodeNavigation")]
        public virtual ICollection<TmRegzonemap> TmRegzonemap { get; set; }
        [InverseProperty("DivisionCodeNavigation")]
        public virtual ICollection<TmUserdefault> TmUserdefault { get; set; }
        [InverseProperty("UnitTypeNavigation")]
        public virtual ICollection<TmUserright> TmUserright { get; set; }
    }
}
