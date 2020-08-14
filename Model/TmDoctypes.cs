using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactPPD.Model
{
    [Table("tm_doctypes")]
    public partial class TmDoctypes
    {
        public TmDoctypes()
        {
            InverseTmDoctypesNavigation = new HashSet<TmDoctypes>();
        }

        [Required]
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [Key]
        [StringLength(10)]
        public string BranchCode { get; set; }
        [Key]
        [Column(TypeName = "int(6)")]
        public int AcYearNo { get; set; }
        [Key]
        [StringLength(6)]
        public string DocType { get; set; }
        [Required]
        [StringLength(35)]
        public string DocName { get; set; }
        [Required]
        [StringLength(2)]
        public string NumGenType { get; set; }
        [Required]
        [StringLength(7)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(1)]
        public string TemplateReq { get; set; }
        [Required]
        [StringLength(10)]
        public string TemplateCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastTransDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromBackDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToBackDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CancelBackDate { get; set; }
        [Required]
        [StringLength(1)]
        public string WorkFlow { get; set; }
        [Column(TypeName = "int(11)")]
        public int NoOfTrans { get; set; }
        [Required]
        [StringLength(2)]
        public string DocCategory { get; set; }
        [Required]
        [StringLength(3)]
        public string DocClass { get; set; }
        [Required]
        [StringLength(12)]
        public string AcCode { get; set; }
        [Required]
        [StringLength(12)]
        public string AcCode1 { get; set; }
        [Required]
        [StringLength(35)]
        public string Descn { get; set; }
        [StringLength(5)]
        public string BaseDocType { get; set; }
        [StringLength(1)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(1)]
        public string DenomReq { get; set; }

        [ForeignKey(nameof(AcCode1))]
        [InverseProperty(nameof(TmAccountsdetail.TmDoctypesAcCode11))]
        public virtual TmAccountsdetail AcCode11 { get; set; }
        [ForeignKey(nameof(AcCode1))]
        [InverseProperty(nameof(TmAccounts.TmDoctypesAcCode1Navigation))]
        public virtual TmAccounts AcCode1Navigation { get; set; }
        [ForeignKey(nameof(AcCode))]
        [InverseProperty(nameof(TmAccountsdetail.TmDoctypesAcCode2))]
        public virtual TmAccountsdetail AcCode2 { get; set; }
        [ForeignKey(nameof(AcCode))]
        [InverseProperty(nameof(TmAccounts.TmDoctypesAcCodeNavigation))]
        public virtual TmAccounts AcCodeNavigation { get; set; }
        [ForeignKey(nameof(BranchCode))]
        [InverseProperty(nameof(TmBranch.TmDoctypes))]
        public virtual TmBranch BranchCodeNavigation { get; set; }
        [ForeignKey(nameof(CompanyCode))]
        [InverseProperty(nameof(TmCompany.TmDoctypes))]
        public virtual TmCompany CompanyCodeNavigation { get; set; }
        [ForeignKey(nameof(DocClass))]
        [InverseProperty(nameof(TmDocclass.TmDoctypes))]
        public virtual TmDocclass DocClassNavigation { get; set; }
        [ForeignKey("BranchCode,AcYearNo")]
        [InverseProperty("TmDoctypes")]
        public virtual TmAcyearset TmAcyearset { get; set; }
        [ForeignKey("BranchCode,AcYearNo,BaseDocType")]
        [InverseProperty(nameof(TmDoctypes.InverseTmDoctypesNavigation))]
        public virtual TmDoctypes TmDoctypesNavigation { get; set; }
        [InverseProperty(nameof(TmDoctypes.TmDoctypesNavigation))]
        public virtual ICollection<TmDoctypes> InverseTmDoctypesNavigation { get; set; }
    }
}
