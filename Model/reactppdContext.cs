using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReactPPD.Model
{
    public partial class reactppdContext : DbContext
    {
               public reactppdContext(DbContextOptions<reactppdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TmAcbrmap> TmAcbrmap { get; set; }
        public virtual DbSet<TmAccounts> TmAccounts { get; set; }
        public virtual DbSet<TmAccountsdetail> TmAccountsdetail { get; set; }
        public virtual DbSet<TmAcgroup> TmAcgroup { get; set; }
        public virtual DbSet<TmAcyearset> TmAcyearset { get; set; }
        public virtual DbSet<TmAnalysis> TmAnalysis { get; set; }
        public virtual DbSet<TmBranch> TmBranch { get; set; }
        public virtual DbSet<TmCompany> TmCompany { get; set; }
        public virtual DbSet<TmCostcenter> TmCostcenter { get; set; }
        public virtual DbSet<TmCurrency> TmCurrency { get; set; }
        public virtual DbSet<TmDesgn> TmDesgn { get; set; }
        public virtual DbSet<TmDivision> TmDivision { get; set; }
        public virtual DbSet<TmDocclass> TmDocclass { get; set; }
        public virtual DbSet<TmDoctables> TmDoctables { get; set; }
        public virtual DbSet<TmDoctypes> TmDoctypes { get; set; }
        public virtual DbSet<TmEmployee> TmEmployee { get; set; }
        public virtual DbSet<TmGcm> TmGcm { get; set; }
        public virtual DbSet<TmGcmtype> TmGcmtype { get; set; }
        public virtual DbSet<TmGodown> TmGodown { get; set; }
        public virtual DbSet<TmGrpschedule> TmGrpschedule { get; set; }
        public virtual DbSet<TmItem> TmItem { get; set; }
        public virtual DbSet<TmItemcategory> TmItemcategory { get; set; }
        public virtual DbSet<TmItemnature> TmItemnature { get; set; }
        public virtual DbSet<TmItemtype> TmItemtype { get; set; }
        public virtual DbSet<TmMeats> TmMeats { get; set; }
        public virtual DbSet<TmMenuitem> TmMenuitem { get; set; }
        public virtual DbSet<TmPartytype> TmPartytype { get; set; }
        public virtual DbSet<TmPlace> TmPlace { get; set; }
        public virtual DbSet<TmProdnature> TmProdnature { get; set; }
        public virtual DbSet<TmReason> TmReason { get; set; }
        public virtual DbSet<TmRegion> TmRegion { get; set; }
        public virtual DbSet<TmRegionmap> TmRegionmap { get; set; }
        public virtual DbSet<TmRegzone> TmRegzone { get; set; }
        public virtual DbSet<TmRegzonemap> TmRegzonemap { get; set; }
        public virtual DbSet<TmTdsac> TmTdsac { get; set; }
        public virtual DbSet<TmTdsnature> TmTdsnature { get; set; }
        public virtual DbSet<TmUom> TmUom { get; set; }
        public virtual DbSet<TmUser> TmUser { get; set; }
        public virtual DbSet<TmUserdefault> TmUserdefault { get; set; }
        public virtual DbSet<TmUserdivmap> TmUserdivmap { get; set; }
        public virtual DbSet<TmUserright> TmUserright { get; set; }
        public virtual DbSet<TmVendor> TmVendor { get; set; }
        public virtual DbSet<TmZone> TmZone { get; set; }
        public virtual DbSet<TtShedready> TtShedready { get; set; }
              protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TmAcbrmap>(entity =>
            {
                entity.HasKey(e => new { e.BranchCode, e.AccountCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AccountName)
                    .HasName("FK_AcBrMapAcName");

                entity.HasIndex(e => new { e.AccountCode, e.Actype })
                    .HasName("FK_AcBrMapAcType");

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.AccountName).IsUnicode(false);

                entity.Property(e => e.Actype).IsUnicode(false);

                entity.Property(e => e.BrShortName).IsUnicode(false);

                entity.Property(e => e.BroSal)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.IsBudgetable)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.IsTrans)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.HasOne(d => d.AccountCodeNavigation)
                    .WithMany(p => p.TmAcbrmapAccountCodeNavigation)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcBrMap");

                entity.HasOne(d => d.AccountCode1)
                    .WithMany(p => p.TmAcbrmap)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcBrMapAcDtl");

                entity.HasOne(d => d.AccountNameNavigation)
                    .WithMany(p => p.TmAcbrmapAccountNameNavigation)
                    .HasPrincipalKey(p => p.AccountName)
                    .HasForeignKey(d => d.AccountName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcBrMapAcName");

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmAcbrmap)
                    .HasForeignKey(d => d.BranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAcBrMapBranch");

                entity.HasOne(d => d.Ac)
                    .WithMany(p => p.TmAcbrmapAc)
                    .HasPrincipalKey(p => new { p.AccountCode, p.AcType })
                    .HasForeignKey(d => new { d.AccountCode, d.Actype })
                    .HasConstraintName("FK_AcBrMapAcType");
            });

            modelBuilder.Entity<TmAccounts>(entity =>
            {
                entity.HasKey(e => e.AccountCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AccountName)
                    .HasName("UK_AcountsAccountName")
                    .IsUnique();

                entity.HasIndex(e => e.ConsGrpCodeCr)
                    .HasName("FK_AccountsConsGrpCodeCr");

                entity.HasIndex(e => e.ConsGrpCodeDr)
                    .HasName("FK_AccountsConsGrpCodeDr");

                entity.HasIndex(e => new { e.AccNature, e.AccountCode })
                    .HasName("UK_AccountsAccNature")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountCode, e.AcType })
                    .HasName("UK_AccountsAcType")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountName, e.AccountCode })
                    .HasName("UK_AccountsAcNameCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.AnalCode1, e.AccountCode })
                    .HasName("UK_AccountsAnalCode1")
                    .IsUnique();

                entity.HasIndex(e => new { e.AnalCode2, e.AccountCode })
                    .HasName("UK_AccountsAnalCode2")
                    .IsUnique();

                entity.HasIndex(e => new { e.AnalCode3, e.AccountCode })
                    .HasName("UK_AccountsAnalCode3")
                    .IsUnique();

                entity.HasIndex(e => new { e.AnalCode4, e.AccountCode })
                    .HasName("UK_AccountsAnalCode4")
                    .IsUnique();

                entity.HasIndex(e => new { e.AnalCode5, e.AccountCode })
                    .HasName("UK_AccountsAnalCode5")
                    .IsUnique();

                entity.HasIndex(e => new { e.BeAcOnGl, e.AccountCode })
                    .HasName("UK_AccountsBeAcOnGlAc")
                    .IsUnique();

                entity.HasIndex(e => new { e.BillAlloc, e.AccountCode })
                    .HasName("UK_AccountsBillAlloc")
                    .IsUnique();

                entity.HasIndex(e => new { e.CcReq, e.AccountCode })
                    .HasName("UK_AccountsCcReq")
                    .IsUnique();

                entity.HasIndex(e => new { e.ControlAc, e.AccountCode })
                    .HasName("UK_AccountsControlAc")
                    .IsUnique();

                entity.HasIndex(e => new { e.Exported, e.AccountCode })
                    .HasName("UK_AccountsExported")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpCodeCr, e.AccountCode })
                    .HasName("UK_AccountsGrpCodeCr")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpCodeDr, e.AccountCode })
                    .HasName("UK_AccountsGrpCodeDr")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.AccountCode })
                    .HasName("UK_AccountsIsActive")
                    .IsUnique();

                entity.HasIndex(e => new { e.PartyType, e.AccountCode })
                    .HasName("UK_AccountsPartyType")
                    .IsUnique();

                entity.HasIndex(e => new { e.BeAcOnGl, e.AccountCode, e.AccountName })
                    .HasName("UK_AccountsBeAcOnGlName")
                    .IsUnique();

                entity.HasIndex(e => new { e.BeAcOnGl, e.BeAcOnDesc, e.AccountCode })
                    .HasName("UK_AccountsBeAcOnGlDesc")
                    .IsUnique();

                entity.HasIndex(e => new { e.BeAcOnGl, e.ControlAc, e.AccountCode })
                    .HasName("UK_AccountsBeAcOnGlClAc")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountCode, e.AcType, e.GrpCodeCr, e.GrpCodeDr })
                    .HasName("UK_AccountTypeGrpCrDr")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountCode, e.ControlAc, e.IsActive, e.EmpCode })
                    .HasName("UK_EmpAcCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpCodeCr, e.ControlAc, e.AcType, e.AccountCode })
                    .HasName("UK_AccountsGdcAcType")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpCodeDr, e.ControlAc, e.AcType, e.AccountCode })
                    .HasName("UK_AccountsGddAcType")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpCodeDr, e.GrpCodeCr, e.ControlAc, e.AcType, e.AccountCode })
                    .HasName("UK_AccountsGrpCodeAcType")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountCode, e.AccountName, e.GrpCodeCr, e.GrpCodeDr, e.AcType, e.ControlAc })
                    .HasName("UK_AccountsAcName")
                    .IsUnique();

                entity.HasIndex(e => new { e.ControlAc, e.AcType, e.SubLedCategory, e.PartyType, e.GrpCodeDr, e.GrpCodeCr, e.AccountCode, e.BeAcOnGl })
                    .HasName("UK_AccountsContGrpAcType")
                    .IsUnique();

                entity.HasIndex(e => new { e.PartyType, e.ControlAc, e.AnalCode1, e.AnalCode2, e.AnalCode3, e.AnalCode4, e.AnalCode5, e.AccountCode })
                    .HasName("UK_AccountsPartyControl")
                    .IsUnique();

                entity.HasIndex(e => new { e.PartyType, e.ControlAc, e.AnalCode5, e.AnalCode4, e.AnalCode3, e.AnalCode2, e.AnalCode1, e.AccountName, e.AccountCode })
                    .HasName("UK_AccountsPartyAnal")
                    .IsUnique();

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.AcType).IsUnicode(false);

                entity.Property(e => e.AccNature).IsUnicode(false);

                entity.Property(e => e.AccountName).IsUnicode(false);

                entity.Property(e => e.AnalCode1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE1'");

                entity.Property(e => e.AnalCode2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE2'");

                entity.Property(e => e.AnalCode3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE3'");

                entity.Property(e => e.AnalCode4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE4'");

                entity.Property(e => e.AnalCode5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE5'");

                entity.Property(e => e.AreaCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NIL'");

                entity.Property(e => e.BeAcOnDesc).IsUnicode(false);

                entity.Property(e => e.BeAcOnGl).IsUnicode(false);

                entity.Property(e => e.BeAcOnSl).IsUnicode(false);

                entity.Property(e => e.BeAcOnSlType).IsUnicode(false);

                entity.Property(e => e.BillAlloc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NE'");

                entity.Property(e => e.CcReq)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ConsGrpCodeCr).IsUnicode(false);

                entity.Property(e => e.ConsGrpCodeDr).IsUnicode(false);

                entity.Property(e => e.ControlAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.CreditDays).HasDefaultValueSql("'1'");

                entity.Property(e => e.Currency)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'INR'");

                entity.Property(e => e.EmpCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-00724'");

                entity.Property(e => e.ExpPymt)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NN'");

                entity.Property(e => e.Exported)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.GrpCodeCr).IsUnicode(false);

                entity.Property(e => e.GrpCodeDr).IsUnicode(false);

                entity.Property(e => e.GrpType).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.IsGovtOrg)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.PartyCode).IsUnicode(false);

                entity.Property(e => e.PartyName).IsUnicode(false);

                entity.Property(e => e.PartyType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'O'");

                entity.Property(e => e.PayFavour).IsUnicode(false);

                entity.Property(e => e.SubLedCategory)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NO'");

                entity.Property(e => e.Tds)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.TdsNature).IsUnicode(false);

                entity.Property(e => e.TransControl)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.HasOne(d => d.AnalCode1Navigation)
                    .WithMany(p => p.TmAccountsAnalCode1Navigation)
                    .HasForeignKey(d => d.AnalCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsAnal1");

                entity.HasOne(d => d.AnalCode2Navigation)
                    .WithMany(p => p.TmAccountsAnalCode2Navigation)
                    .HasForeignKey(d => d.AnalCode2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsAnal2");

                entity.HasOne(d => d.AnalCode3Navigation)
                    .WithMany(p => p.TmAccountsAnalCode3Navigation)
                    .HasForeignKey(d => d.AnalCode3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsAnal3");

                entity.HasOne(d => d.AnalCode4Navigation)
                    .WithMany(p => p.TmAccountsAnalCode4Navigation)
                    .HasForeignKey(d => d.AnalCode4)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsAnal4");

                entity.HasOne(d => d.AnalCode5Navigation)
                    .WithMany(p => p.TmAccountsAnalCode5Navigation)
                    .HasForeignKey(d => d.AnalCode5)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsAnal5");

                entity.HasOne(d => d.ConsGrpCodeCrNavigation)
                    .WithMany(p => p.TmAccountsConsGrpCodeCrNavigation)
                    .HasForeignKey(d => d.ConsGrpCodeCr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsConsGrpCodeCr");

                entity.HasOne(d => d.ConsGrpCodeDrNavigation)
                    .WithMany(p => p.TmAccountsConsGrpCodeDrNavigation)
                    .HasForeignKey(d => d.ConsGrpCodeDr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsConsGrpCodeDr");

                entity.HasOne(d => d.ControlAcNavigation)
                    .WithMany(p => p.InverseControlAcNavigation)
                    .HasForeignKey(d => d.ControlAc)
                    .HasConstraintName("FK_AccountsControlAc");

                entity.HasOne(d => d.GrpCodeCrNavigation)
                    .WithMany(p => p.TmAccountsGrpCodeCrNavigation)
                    .HasForeignKey(d => d.GrpCodeCr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsGrpCodeCr");

                entity.HasOne(d => d.GrpCodeDrNavigation)
                    .WithMany(p => p.TmAccountsGrpCodeDrNavigation)
                    .HasForeignKey(d => d.GrpCodeDr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsGrpCodeDr");

                entity.HasOne(d => d.PartyTypeNavigation)
                    .WithMany(p => p.TmAccounts)
                    .HasForeignKey(d => d.PartyType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsPartyType");
            });

            modelBuilder.Entity<TmAccountsdetail>(entity =>
            {
                entity.HasKey(e => e.AccountCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.StateCode, e.AccountCode })
                    .HasName("UK_AccountsStateCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.AccountCode, e.Address1, e.Address2, e.Address3, e.StateCode })
                    .HasName("UK_AccountsAddress")
                    .IsUnique();

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.AcRefNo).IsUnicode(false);

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.Address3).IsUnicode(false);

                entity.Property(e => e.BankAdd1).IsUnicode(false);

                entity.Property(e => e.BankAdd2).IsUnicode(false);

                entity.Property(e => e.BankAdd3).IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE1'");

                entity.Property(e => e.CityCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONET'");

                entity.Property(e => e.ContactPer).IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEO'");

                entity.Property(e => e.CstNo).IsUnicode(false);

                entity.Property(e => e.DistCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONED'");

                entity.Property(e => e.FavourOf).IsUnicode(false);

                entity.Property(e => e.FaxNo).IsUnicode(false);

                entity.Property(e => e.GstNo).IsUnicode(false);

                entity.Property(e => e.LandMark).IsUnicode(false);

                entity.Property(e => e.LstNo).IsUnicode(false);

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.Owner).IsUnicode(false);

                entity.Property(e => e.PanNo).IsUnicode(false);

                entity.Property(e => e.PayFavour).IsUnicode(false);

                entity.Property(e => e.PayableAt)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONET'");

                entity.Property(e => e.PdcAppNo).IsUnicode(false);

                entity.Property(e => e.PdcNo).IsUnicode(false);

                entity.Property(e => e.PdcRefNo).IsUnicode(false);

                entity.Property(e => e.PhoneNo).IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEZ'");

                entity.Property(e => e.StateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONES'");

                entity.Property(e => e.TalukCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEM'");

                entity.Property(e => e.VatNo).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);

                entity.HasOne(d => d.AccountCodeNavigation)
                    .WithOne(p => p.TmAccountsdetail)
                    .HasForeignKey<TmAccountsdetail>(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountsDetailCode");
            });

            modelBuilder.Entity<TmAcgroup>(entity =>
            {
                entity.HasKey(e => e.GrpCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.GrpType, e.GrpCode })
                    .HasName("UK_AcGroupGrpType")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.GrpCode })
                    .HasName("UK_AcGroupIsActive")
                    .IsUnique();

                entity.HasIndex(e => new { e.PrtGrpCode, e.GrpCode })
                    .HasName("UK_AcGroupPrtGrpCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.SchNo, e.GrpCode })
                    .HasName("UK_AcGroupSchNo")
                    .IsUnique();

                entity.HasIndex(e => new { e.SchReq, e.GrpCode })
                    .HasName("UK_AcGroupSchReq")
                    .IsUnique();

                entity.HasIndex(e => new { e.UsageType, e.GrpCode })
                    .HasName("UK_AcGroupUsageType")
                    .IsUnique();

                entity.HasIndex(e => new { e.GrpType, e.GrpName, e.GrpCode })
                    .HasName("UK_AcGroupsGrpType")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.GrpName, e.GrpCode })
                    .HasName("UK_AcGroupName")
                    .IsUnique();

                entity.Property(e => e.GrpCode).IsUnicode(false);

                entity.Property(e => e.ExpandGrp).IsUnicode(false);

                entity.Property(e => e.GrpName).IsUnicode(false);

                entity.Property(e => e.GrpOrder).HasDefaultValueSql("'0'");

                entity.Property(e => e.GrpType).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.PrtGrpCode).IsUnicode(false);

                entity.Property(e => e.SchReq).IsUnicode(false);

                entity.Property(e => e.UsageType).IsUnicode(false);

                entity.HasOne(d => d.PrtGrpCodeNavigation)
                    .WithMany(p => p.InversePrtGrpCodeNavigation)
                    .HasForeignKey(d => d.PrtGrpCode)
                    .HasConstraintName("FK_AcGroupPrtGrpCode");

                entity.HasOne(d => d.SchNoNavigation)
                    .WithMany(p => p.TmAcgroup)
                    .HasForeignKey(d => d.SchNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AcGroupSchNo");
            });

            modelBuilder.Entity<TmAcyearset>(entity =>
            {
                entity.HasKey(e => new { e.BranchCode, e.AcYearNo })
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.BranchCode, e.IsActive, e.AcYearNo })
                    .HasName("UK_AcYearSetBrcode")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.BranchCode, e.AcYearNo })
                    .HasName("UK_AcYearSetIsActive")
                    .IsUnique();

                entity.HasIndex(e => new { e.BranchCode, e.IsActive, e.AcYearNo, e.FromMm, e.FromYyyy, e.ToMm, e.ToYyyy })
                    .HasName("UK_AcYearSetPeriod")
                    .IsUnique();

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.AllocTryy).IsUnicode(false);

                entity.Property(e => e.EntryAllow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.OpBalAllocTran).IsUnicode(false);

                entity.Property(e => e.OpstkTrans)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmAcyearset)
                    .HasForeignKey(d => d.BranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_AcYearSetBranchCode");
            });

            modelBuilder.Entity<TmAnalysis>(entity =>
            {
                entity.HasKey(e => e.AnalCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AnalName)
                    .HasName("UK_AnalysisName")
                    .IsUnique();

                entity.HasIndex(e => new { e.Category, e.AnalCode })
                    .HasName("UK_AnalysisCategory")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.AnalCode })
                    .HasName("UK_AnalysisIsActive")
                    .IsUnique();

                entity.HasIndex(e => new { e.Type, e.AnalCode })
                    .HasName("UK_AnalysisType")
                    .IsUnique();

                entity.Property(e => e.AnalCode).IsUnicode(false);

                entity.Property(e => e.AnalName).IsUnicode(false);

                entity.Property(e => e.Category).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Type)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'ACC'");
            });

            modelBuilder.Entity<TmBranch>(entity =>
            {
                entity.HasKey(e => e.BranchCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.BranchName)
                    .HasName("BranchName")
                    .IsUnique();

                entity.HasIndex(e => e.ShortName)
                    .HasName("BranchShortName")
                    .IsUnique();

                entity.HasIndex(e => new { e.BrType, e.BranchCode })
                    .HasName("BranchBrType")
                    .IsUnique();

                entity.HasIndex(e => new { e.BranchCode, e.StateCode })
                    .HasName("BranchBsStateCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.BranchName, e.StateCode })
                    .HasName("BranchBrState")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyCode, e.BranchCode })
                    .HasName("BranchCompanyCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.DivCode, e.BranchCode })
                    .HasName("BranchDivCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.BranchCode })
                    .HasName("BranchIsActive")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegZoneCode, e.BranchCode })
                    .HasName("BranchRegZoneCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegionCode, e.BranchCode })
                    .HasName("BranchRegionCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.ZoneCode, e.BranchCode })
                    .HasName("BranchZoneCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.DivCode, e.ShortName, e.BranchCode })
                    .HasName("CompanyCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegZoneCode, e.ZoneCode, e.RegionCode })
                    .HasName("BranchDivcomp")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegionCode, e.GcaCode, e.BranchCode })
                    .HasName("BranchCACCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.DivCode, e.RegZoneCode, e.RegionCode, e.BranchCode, e.BrType, e.BranchName, e.IsActive })
                    .HasName("BranchRegZoneReg")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyCode, e.RegZoneCode, e.RegionCode, e.ZoneCode, e.DivCode, e.BrType, e.BranchCode, e.GcaCode, e.IsActive })
                    .HasName("Branch")
                    .IsUnique();

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.AcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''NONE'''");

                entity.Property(e => e.AcStDate).HasDefaultValueSql("'1947-08-15'");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.Address3).IsUnicode(false);

                entity.Property(e => e.AlternateDns).IsUnicode(false);

                entity.Property(e => e.BeAcOnCode).IsUnicode(false);

                entity.Property(e => e.BrAcUser)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''NONE'''");

                entity.Property(e => e.BrInCharge)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''NONE'''");

                entity.Property(e => e.BrType).IsUnicode(false);

                entity.Property(e => e.BranchName).IsUnicode(false);

                entity.Property(e => e.Cessper).HasDefaultValueSql("'0.00'");

                entity.Property(e => e.CityCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONET'");

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.Connectivity)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''NONE'''");

                entity.Property(e => e.CountryCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEO'");

                entity.Property(e => e.CstNo).IsUnicode(false);

                entity.Property(e => e.Currency).IsUnicode(false);

                entity.Property(e => e.DailUpNo).IsUnicode(false);

                entity.Property(e => e.DistCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONED'");

                entity.Property(e => e.DivCode).IsUnicode(false);

                entity.Property(e => e.EndIp).IsUnicode(false);

                entity.Property(e => e.Esicoverage).IsUnicode(false);

                entity.Property(e => e.ExciseDivn).IsUnicode(false);

                entity.Property(e => e.ExciseRange).IsUnicode(false);

                entity.Property(e => e.FaxNo).IsUnicode(false);

                entity.Property(e => e.GcaCode).IsUnicode(false);

                entity.Property(e => e.GetWayIp).IsUnicode(false);

                entity.Property(e => e.InsureId).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''A'''");

                entity.Property(e => e.LstNo).IsUnicode(false);

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.Mktgmage).HasDefaultValueSql("'2'");

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.NoOfComp).HasDefaultValueSql("'1'");

                entity.Property(e => e.NwmzPoEndDt)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''31-12'''");

                entity.Property(e => e.NwmzPoEndDt1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''15-06'''");

                entity.Property(e => e.NwmzPostDt1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''15-02'''");

                entity.Property(e => e.NwmzPostdt)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''15-09'''");

                entity.Property(e => e.OfficeId)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''RO'''");

                entity.Property(e => e.OpTransSys)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''N'''");

                entity.Property(e => e.OrdNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PanNo).IsUnicode(false);

                entity.Property(e => e.ParBrCode).IsUnicode(false);

                entity.Property(e => e.PhoneNo).IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEZ'");

                entity.Property(e => e.PrimaryDns).IsUnicode(false);

                entity.Property(e => e.PtaxCoverage).IsUnicode(false);

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.SerProvider).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.StartIp).IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONES'");

                entity.Property(e => e.SubnetMask).IsUnicode(false);

                entity.Property(e => e.TalukCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEM'");

                entity.Property(e => e.TinNo).IsUnicode(false);

                entity.Property(e => e.VatNo).IsUnicode(false);

                entity.Property(e => e.ZoneCode).IsUnicode(false);
            });

            modelBuilder.Entity<TmCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CompanyName)
                    .HasName("CompanyName")
                    .IsUnique();

                entity.HasIndex(e => e.GcacCode)
                    .HasName("FKCompanyGcacCode");

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.AdminOffAdd1).IsUnicode(false);

                entity.Property(e => e.AdminOffAdd2).IsUnicode(false);

                entity.Property(e => e.AdminOffAdd3).IsUnicode(false);

                entity.Property(e => e.CeNo).IsUnicode(false);

                entity.Property(e => e.CityCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONET'");

                entity.Property(e => e.CompanyName).IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEO'");

                entity.Property(e => e.CstNo).IsUnicode(false);

                entity.Property(e => e.DistCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONED'");

                entity.Property(e => e.DlNo).IsUnicode(false);

                entity.Property(e => e.FaxNo).IsUnicode(false);

                entity.Property(e => e.GcacCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.ItCircle).IsUnicode(false);

                entity.Property(e => e.LstNo).IsUnicode(false);

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.Page1).HasDefaultValueSql("'7'");

                entity.Property(e => e.Page2).HasDefaultValueSql("'14'");

                entity.Property(e => e.Page3).HasDefaultValueSql("'21'");

                entity.Property(e => e.Page4).HasDefaultValueSql("'30'");

                entity.Property(e => e.Page5).HasDefaultValueSql("'60'");

                entity.Property(e => e.Page6).HasDefaultValueSql("'90'");

                entity.Property(e => e.PanNo).IsUnicode(false);

                entity.Property(e => e.PhoneNo).IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEZ'");

                entity.Property(e => e.Rage1).HasDefaultValueSql("'7'");

                entity.Property(e => e.Rage2).HasDefaultValueSql("'14'");

                entity.Property(e => e.Rage3).HasDefaultValueSql("'21'");

                entity.Property(e => e.Rage4).HasDefaultValueSql("'30'");

                entity.Property(e => e.Rage5).HasDefaultValueSql("'60'");

                entity.Property(e => e.Rage6).HasDefaultValueSql("'90'");

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONES'");

                entity.Property(e => e.TalukCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEM'");

                entity.Property(e => e.VatNo).IsUnicode(false);

                entity.Property(e => e.WebSite).IsUnicode(false);

                entity.HasOne(d => d.GcacCodeNavigation)
                    .WithMany(p => p.TmCompany)
                    .HasForeignKey(d => d.GcacCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCompanyGcacCode");
            });

            modelBuilder.Entity<TmCostcenter>(entity =>
            {
                entity.HasKey(e => e.CcCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CcName)
                    .HasName("UK_CostCenterName")
                    .IsUnique();

                entity.HasIndex(e => e.GprtCcCode)
                    .HasName("FK_CostCenterGprTcc");

                entity.HasIndex(e => e.PrtCcCode)
                    .HasName("FK_CostCenterPrtCc");

                entity.HasIndex(e => e.RegionCode)
                    .HasName("FK_CostCenterRegCode");

                entity.Property(e => e.CcCode).IsUnicode(false);

                entity.Property(e => e.CcName).IsUnicode(false);

                entity.Property(e => e.CcType).IsUnicode(false);

                entity.Property(e => e.GprtCcCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.PrtCcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.HasOne(d => d.GprtCcCodeNavigation)
                    .WithMany(p => p.InverseGprtCcCodeNavigation)
                    .HasForeignKey(d => d.GprtCcCode)
                    .HasConstraintName("FK_CostCenterGprTcc");

                entity.HasOne(d => d.PrtCcCodeNavigation)
                    .WithMany(p => p.InversePrtCcCodeNavigation)
                    .HasForeignKey(d => d.PrtCcCode)
                    .HasConstraintName("FK_CostCenterPrtCc");

                entity.HasOne(d => d.RegionCodeNavigation)
                    .WithMany(p => p.TmCostcenter)
                    .HasForeignKey(d => d.RegionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CostCenterRegCode");
            });

            modelBuilder.Entity<TmCurrency>(entity =>
            {
                entity.HasKey(e => e.Currency)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CurrencyDesc)
                    .HasName("UK_CurrencyDesc")
                    .IsUnique();

                entity.Property(e => e.Currency).IsUnicode(false);

                entity.Property(e => e.CurrencyDesc).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.ShortName).IsUnicode(false);
            });

            modelBuilder.Entity<TmDesgn>(entity =>
            {
                entity.HasKey(e => e.DesgnCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.DesgName)
                    .HasName("DesgName")
                    .IsUnique();

                entity.Property(e => e.DesgnCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''ENABLE'''");

                entity.Property(e => e.DesgName).IsUnicode(false);

                entity.Property(e => e.DesgnGrpCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<TmDivision>(entity =>
            {
                entity.HasKey(e => e.DivCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.DivName)
                    .HasName("DivisionName")
                    .IsUnique();

                entity.HasIndex(e => e.PrtDivCode)
                    .HasName("DivisionPrtDivCode");

                entity.HasIndex(e => e.ShortName)
                    .HasName("DivisionShort")
                    .IsUnique();

                entity.HasIndex(e => new { e.DivCode, e.DivName, e.IsActive })
                    .HasName("DivisionCodeName")
                    .IsUnique();

                entity.Property(e => e.DivCode).IsUnicode(false);

                entity.Property(e => e.BconBusArea).IsUnicode(false);

                entity.Property(e => e.BconDiv).IsUnicode(false);

                entity.Property(e => e.BconIdsFix).IsUnicode(false);

                entity.Property(e => e.Div).IsUnicode(false);

                entity.Property(e => e.DivName).IsUnicode(false);

                entity.Property(e => e.FeedReq)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.OrdNo).HasDefaultValueSql("'0'");

                entity.Property(e => e.PrtDivCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ShortCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.SlNo).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.PrtDivCodeNavigation)
                    .WithMany(p => p.InversePrtDivCodeNavigation)
                    .HasForeignKey(d => d.PrtDivCode)
                    .HasConstraintName("DivisionPrtDivCode");
            });

            modelBuilder.Entity<TmDocclass>(entity =>
            {
                entity.HasKey(e => e.DocClass)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.DocClName)
                    .HasName("UK_DocClassName")
                    .IsUnique();

                entity.Property(e => e.DocClass).IsUnicode(false);

                entity.Property(e => e.DocClName).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<TmDoctables>(entity =>
            {
                entity.HasKey(e => new { e.DocType, e.TableName, e.ProcedureName, e.Operation, e.OrdId })
                    .HasName("PRIMARY");

                entity.Property(e => e.DocType).IsUnicode(false);

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.Property(e => e.ProcedureName).IsUnicode(false);

                entity.Property(e => e.Operation).IsUnicode(false);

                entity.Property(e => e.AddParam)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ExCpmSg).IsUnicode(false);

                entity.Property(e => e.GridColTitle).IsUnicode(false);

                entity.Property(e => e.Header)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.Mrows)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.RowNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.SgOrdId).HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdStatus)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");
            });

            modelBuilder.Entity<TmDoctypes>(entity =>
            {
                entity.HasKey(e => new { e.BranchCode, e.AcYearNo, e.DocType })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AcCode1)
                    .HasName("FK_DocTypesAcCode3");

                entity.HasIndex(e => e.CompanyCode)
                    .HasName("FK_DocTypesCom");

                entity.HasIndex(e => new { e.BranchCode, e.AcYearNo, e.BaseDocType })
                    .HasName("FK_DocTypeBaseDocType");

                entity.HasIndex(e => new { e.AcCode, e.DocType, e.AcYearNo, e.BranchCode })
                    .HasName("UK_DocTypesCodeDocType")
                    .IsUnique();

                entity.HasIndex(e => new { e.DocClass, e.DocType, e.AcYearNo, e.BranchCode })
                    .HasName("UK_DocTypeDocClass")
                    .IsUnique();

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.DocType).IsUnicode(false);

                entity.Property(e => e.AcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.AcCode1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.BaseDocType).IsUnicode(false);

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.DenomReq)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.Descn).IsUnicode(false);

                entity.Property(e => e.DocCategory).IsUnicode(false);

                entity.Property(e => e.DocClass).IsUnicode(false);

                entity.Property(e => e.DocName).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.NumGenType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NY'");

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.TemplateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.TemplateReq).IsUnicode(false);

                entity.Property(e => e.WorkFlow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.HasOne(d => d.AcCodeNavigation)
                    .WithMany(p => p.TmDoctypesAcCodeNavigation)
                    .HasForeignKey(d => d.AcCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesAcCode2");

                entity.HasOne(d => d.AcCode2)
                    .WithMany(p => p.TmDoctypesAcCode2)
                    .HasForeignKey(d => d.AcCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesAcCodeAcDtl");

                entity.HasOne(d => d.AcCode1Navigation)
                    .WithMany(p => p.TmDoctypesAcCode1Navigation)
                    .HasForeignKey(d => d.AcCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesAcCode3");

                entity.HasOne(d => d.AcCode11)
                    .WithMany(p => p.TmDoctypesAcCode11)
                    .HasForeignKey(d => d.AcCode1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesAcCode1AcDtl");

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmDoctypes)
                    .HasForeignKey(d => d.BranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesBranch");

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.TmDoctypes)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesCom");

                entity.HasOne(d => d.DocClassNavigation)
                    .WithMany(p => p.TmDoctypes)
                    .HasForeignKey(d => d.DocClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesDocClass");

                entity.HasOne(d => d.TmAcyearset)
                    .WithMany(p => p.TmDoctypes)
                    .HasForeignKey(d => new { d.BranchCode, d.AcYearNo })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocTypesAcYearNo");

                entity.HasOne(d => d.TmDoctypesNavigation)
                    .WithMany(p => p.InverseTmDoctypesNavigation)
                    .HasForeignKey(d => new { d.BranchCode, d.AcYearNo, d.BaseDocType })
                    .HasConstraintName("FK_DocTypeBaseDocType");
            });

            modelBuilder.Entity<TmEmployee>(entity =>
            {
                entity.HasKey(e => e.EmpCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.AdmRepHead)
                    .HasName("EmployeeAdmh");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("EmployeeBranch");

                entity.HasIndex(e => e.InteCode)
                    .HasName("EmployeeInteCode")
                    .IsUnique();

                entity.HasIndex(e => e.JoinAs)
                    .HasName("EmployeeDesgn");

                entity.HasIndex(e => e.SubBrCode)
                    .HasName("EmployeeSubBr");

                entity.HasIndex(e => e.Substitute)
                    .HasName("EmployeeSubs");

                entity.HasIndex(e => new { e.EmpCode, e.EmpName, e.IsWorking, e.IsActive, e.BranchCode })
                    .HasName("EmployeeEmpCode")
                    .IsUnique();

                entity.Property(e => e.EmpCode).IsUnicode(false);

                entity.Property(e => e.AdmRepHead).IsUnicode(false);

                entity.Property(e => e.BloodGroup).IsUnicode(false);

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.Caste).IsUnicode(false);

                entity.Property(e => e.DocNo).IsUnicode(false);

                entity.Property(e => e.EmpCategory).IsUnicode(false);

                entity.Property(e => e.EmpName).IsUnicode(false);

                entity.Property(e => e.EmpType).IsUnicode(false);

                entity.Property(e => e.EsIno).IsUnicode(false);

                entity.Property(e => e.FunCrepHead).IsUnicode(false);

                entity.Property(e => e.GraduatyNo).IsUnicode(false);

                entity.Property(e => e.IceContact).IsUnicode(false);

                entity.Property(e => e.InteCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.IsWorking).IsUnicode(false);

                entity.Property(e => e.JoinAs).IsUnicode(false);

                entity.Property(e => e.LastSalDrawn)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.LeaReason).IsUnicode(false);

                entity.Property(e => e.LicenseNo).IsUnicode(false);

                entity.Property(e => e.LwfContr).IsUnicode(false);

                entity.Property(e => e.LwfContrNo).IsUnicode(false);

                entity.Property(e => e.MailIdOff).IsUnicode(false);

                entity.Property(e => e.MailIdPer).IsUnicode(false);

                entity.Property(e => e.MartialStatus).IsUnicode(false);

                entity.Property(e => e.MobileNet).IsUnicode(false);

                entity.Property(e => e.MobileOff).IsUnicode(false);

                entity.Property(e => e.Nationality).IsUnicode(false);

                entity.Property(e => e.OldPfNo).IsUnicode(false);

                entity.Property(e => e.PaCode).IsUnicode(false);

                entity.Property(e => e.PanNo).IsUnicode(false);

                entity.Property(e => e.PassPortNo).IsUnicode(false);

                entity.Property(e => e.PfNo).IsUnicode(false);

                entity.Property(e => e.PfSsnNo).IsUnicode(false);

                entity.Property(e => e.Religion).IsUnicode(false);

                entity.Property(e => e.SalPayMode).IsUnicode(false);

                entity.Property(e => e.SalProcess)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.SalaryAcBank).IsUnicode(false);

                entity.Property(e => e.SalaryAcNo).IsUnicode(false);

                entity.Property(e => e.Sex).IsUnicode(false);

                entity.Property(e => e.ShiftType).IsUnicode(false);

                entity.Property(e => e.SubBrCode).IsUnicode(false);

                entity.Property(e => e.Substitute).IsUnicode(false);

                entity.Property(e => e.SupanuationNo).IsUnicode(false);

                entity.Property(e => e.SwfContr).IsUnicode(false);

                entity.Property(e => e.SwfContrNo).IsUnicode(false);

                entity.Property(e => e.VehNo).IsUnicode(false);

                entity.Property(e => e.VehType).IsUnicode(false);

                entity.Property(e => e.Vehclass).IsUnicode(false);

                entity.Property(e => e.Woff).IsUnicode(false);

                entity.HasOne(d => d.AdmRepHeadNavigation)
                    .WithMany(p => p.TmEmployeeAdmRepHeadNavigation)
                    .HasForeignKey(d => d.AdmRepHead)
                    .HasConstraintName("EmployeeAdmh");

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmEmployeeBranchCodeNavigation)
                    .HasForeignKey(d => d.BranchCode)
                    .HasConstraintName("EmployeeBranch");

                entity.HasOne(d => d.JoinAsNavigation)
                    .WithMany(p => p.TmEmployeeJoinAsNavigation)
                    .HasForeignKey(d => d.JoinAs)
                    .HasConstraintName("EmployeeDesgn");

                entity.HasOne(d => d.SubBrCodeNavigation)
                    .WithMany(p => p.TmEmployeeSubBrCodeNavigation)
                    .HasForeignKey(d => d.SubBrCode)
                    .HasConstraintName("EmployeeSubBr");

                entity.HasOne(d => d.SubstituteNavigation)
                    .WithMany(p => p.TmEmployeeSubstituteNavigation)
                    .HasForeignKey(d => d.Substitute)
                    .HasConstraintName("EmployeeSubs");
            });

            modelBuilder.Entity<TmGcm>(entity =>
            {
                entity.HasKey(e => e.GcmCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.GcmType, e.SlNo, e.GcmCode })
                    .HasName("UK_GcmGcmCodeSlNo")
                    .IsUnique();

                entity.HasIndex(e => new { e.GcmType, e.GcmCode, e.GcmDesc, e.IsActive })
                    .HasName("UK_GcmGcmTypeCode")
                    .IsUnique();

                entity.Property(e => e.GcmCode).IsUnicode(false);

                entity.Property(e => e.GcmDesc).IsUnicode(false);

                entity.Property(e => e.GcmType).IsUnicode(false);

                entity.Property(e => e.Grp).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.SlNo).HasDefaultValueSql("'1'");

                entity.HasOne(d => d.GcmTypeNavigation)
                    .WithMany(p => p.TmGcm)
                    .HasForeignKey(d => d.GcmType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGcmGcmType");
            });

            modelBuilder.Entity<TmGcmtype>(entity =>
            {
                entity.HasKey(e => e.GcmType)
                    .HasName("PRIMARY");

                entity.Property(e => e.GcmType).IsUnicode(false);

                entity.Property(e => e.CodeSize).HasDefaultValueSql("'10'");

                entity.Property(e => e.Descn).IsUnicode(false);

                entity.Property(e => e.EnteredBy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'D'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Usage)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'ALL'");
            });

            modelBuilder.Entity<TmGodown>(entity =>
            {
                entity.HasKey(e => e.GoDownCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.BranchCode, e.GoDownCode })
                    .HasName("UK_GoDownBcgName")
                    .IsUnique();

                entity.HasIndex(e => new { e.BranchCode, e.GoDownType, e.IsActive, e.GoDownName })
                    .HasName("UK_GoDownBcgNameType")
                    .IsUnique();

                entity.Property(e => e.GoDownCode).IsUnicode(false);

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.Address3).IsUnicode(false);

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.CcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.CityCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ContactPer).IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.DistCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.FaxNo).IsUnicode(false);

                entity.Property(e => e.GoDownName).IsUnicode(false);

                entity.Property(e => e.GoDownType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'MG'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.PhoneNo).IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.TalukCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.WeighBridge)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'WC'");
            });

            modelBuilder.Entity<TmGrpschedule>(entity =>
            {
                entity.HasKey(e => e.SchNo)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.PrtSchNo)
                    .HasName("FKGrpSchParent");

                entity.HasIndex(e => e.SchName)
                    .HasName("UK_GrpSchName")
                    .IsUnique();

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.SchName).IsUnicode(false);

                entity.HasOne(d => d.PrtSchNoNavigation)
                    .WithMany(p => p.InversePrtSchNoNavigation)
                    .HasForeignKey(d => d.PrtSchNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGrpSchParent");
            });

            modelBuilder.Entity<TmItem>(entity =>
            {
                entity.HasKey(e => e.ItemCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ContCode1)
                    .HasName("FK_ItemCont1");

                entity.HasIndex(e => e.ContCode2)
                    .HasName("FK_ItemCont2");

                entity.HasIndex(e => e.ItemName)
                    .HasName("UK_ItemName")
                    .IsUnique();

                entity.HasIndex(e => e.PmxCode1)
                    .HasName("FK_ItemPmx1");

                entity.HasIndex(e => e.PmxCode2)
                    .HasName("FK_ItemPmx2");

                entity.HasIndex(e => e.ProdNature)
                    .HasName("FK_ItemProdNature");

                entity.HasIndex(e => e.PrtItem)
                    .HasName("FK_ItemPrtItem");

                entity.HasIndex(e => e.UomBig)
                    .HasName("FK_ItemUomBig");

                entity.HasIndex(e => e.UomPur)
                    .HasName("FK_ItemUomPur");

                entity.HasIndex(e => e.UomSmall)
                    .HasName("FK_ItemUomSmall");

                entity.HasIndex(e => e.UomStk)
                    .HasName("FK_ItemItemUomStk");

                entity.HasIndex(e => new { e.Category, e.ItemCode })
                    .HasName("UK_ItemCat")
                    .IsUnique();

                entity.HasIndex(e => new { e.ItemCode, e.UomPur })
                    .HasName("UK_ItemNamePurStk")
                    .IsUnique();

                entity.HasIndex(e => new { e.ItemGroup, e.ItemCode })
                    .HasName("UK_ItemGrp")
                    .IsUnique();

                entity.HasIndex(e => new { e.ItemType, e.ItemCode })
                    .HasName("UK_ItemType")
                    .IsUnique();

                entity.HasIndex(e => new { e.ItemCode, e.ItemName, e.UomStk })
                    .HasName("UK_ItemUomStk")
                    .IsUnique();

                entity.HasIndex(e => new { e.ItemType, e.Category, e.ItemGroup, e.PrtItem, e.ItemCode, e.CashPur, e.IsActive })
                    .HasName("UK_Item")
                    .IsUnique();

                entity.Property(e => e.ItemCode).IsUnicode(false);

                entity.Property(e => e.Abc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.AcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.AcPostBase)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'T'");

                entity.Property(e => e.CashPur)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.Category).IsUnicode(false);

                entity.Property(e => e.Cess)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ContCap).HasDefaultValueSql("'0.0000'");

                entity.Property(e => e.ContCode1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ContCode2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ContWtConf)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.Div)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.Division).IsUnicode(false);

                entity.Property(e => e.ExpDays).HasDefaultValueSql("'90'");

                entity.Property(e => e.Fsn)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.HsnCode).IsUnicode(false);

                entity.Property(e => e.InReq)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.InSpn).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.ItemGroup).IsUnicode(false);

                entity.Property(e => e.ItemName).IsUnicode(false);

                entity.Property(e => e.ItemSpec).IsUnicode(false);

                entity.Property(e => e.ItemType).IsUnicode(false);

                entity.Property(e => e.Nature)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.PartNo).IsUnicode(false);

                entity.Property(e => e.PmxCode1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.PmxCode2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ProdNature)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'O'");

                entity.Property(e => e.PrtItem)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ShortName)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'S'");

                entity.Property(e => e.StProdCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.UomBig).IsUnicode(false);

                entity.Property(e => e.UomPur).IsUnicode(false);

                entity.Property(e => e.UomRelation).HasDefaultValueSql("'1.0000'");

                entity.Property(e => e.UomSmall).IsUnicode(false);

                entity.Property(e => e.UomStk).IsUnicode(false);

                entity.Property(e => e.VisInSpn).IsUnicode(false);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.TmItem)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemCategory");

                entity.HasOne(d => d.ContCode1Navigation)
                    .WithMany(p => p.InverseContCode1Navigation)
                    .HasForeignKey(d => d.ContCode1)
                    .HasConstraintName("FK_ItemCont1");

                entity.HasOne(d => d.ContCode2Navigation)
                    .WithMany(p => p.InverseContCode2Navigation)
                    .HasForeignKey(d => d.ContCode2)
                    .HasConstraintName("FK_ItemCont2");

                entity.HasOne(d => d.PmxCode1Navigation)
                    .WithMany(p => p.InversePmxCode1Navigation)
                    .HasForeignKey(d => d.PmxCode1)
                    .HasConstraintName("FK_ItemPmx1");

                entity.HasOne(d => d.PmxCode2Navigation)
                    .WithMany(p => p.InversePmxCode2Navigation)
                    .HasForeignKey(d => d.PmxCode2)
                    .HasConstraintName("FK_ItemPmx2");

                entity.HasOne(d => d.ProdNatureNavigation)
                    .WithMany(p => p.TmItem)
                    .HasForeignKey(d => d.ProdNature)
                    .HasConstraintName("FK_ItemProdNature");

                entity.HasOne(d => d.PrtItemNavigation)
                    .WithMany(p => p.InversePrtItemNavigation)
                    .HasForeignKey(d => d.PrtItem)
                    .HasConstraintName("FK_ItemPrtItem");

                entity.HasOne(d => d.UomBigNavigation)
                    .WithMany(p => p.TmItemUomBigNavigation)
                    .HasForeignKey(d => d.UomBig)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemUomBig");

                entity.HasOne(d => d.UomPurNavigation)
                    .WithMany(p => p.TmItemUomPurNavigation)
                    .HasForeignKey(d => d.UomPur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemUomPur");

                entity.HasOne(d => d.UomSmallNavigation)
                    .WithMany(p => p.TmItemUomSmallNavigation)
                    .HasForeignKey(d => d.UomSmall)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemUomSmall");

                entity.HasOne(d => d.UomStkNavigation)
                    .WithMany(p => p.TmItemUomStkNavigation)
                    .HasForeignKey(d => d.UomStk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemItemUomStk");
            });

            modelBuilder.Entity<TmItemcategory>(entity =>
            {
                entity.HasKey(e => e.CatgyCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CatgyName)
                    .HasName("UK_ItemCatName")
                    .IsUnique();

                entity.HasIndex(e => e.GprtCode)
                    .HasName("FK_ItemGprtCode");

                entity.HasIndex(e => e.PrtCode)
                    .HasName("FK_ItemPrtCode");

                entity.Property(e => e.CatgyCode).IsUnicode(false);

                entity.Property(e => e.AdvImpCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.AssetAcCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.AssetPreFix)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CapSubCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.CapWipCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.CatgyName).IsUnicode(false);

                entity.Property(e => e.DepDouble)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DepMethod)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DepNor)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DepResCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.DepSingle)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DiscardLossCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.GprtCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.IsAsset)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.LeaseCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.PreOprExpCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.PrtCode).IsUnicode(false);

                entity.Property(e => e.RevResCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.SaleLossCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.SaleProfitCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.HasOne(d => d.GprtCodeNavigation)
                    .WithMany(p => p.InverseGprtCodeNavigation)
                    .HasForeignKey(d => d.GprtCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemGprtCode");

                entity.HasOne(d => d.PrtCodeNavigation)
                    .WithMany(p => p.InversePrtCodeNavigation)
                    .HasForeignKey(d => d.PrtCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemPrtCode");
            });

            modelBuilder.Entity<TmItemnature>(entity =>
            {
                entity.HasKey(e => new { e.ItemCode, e.Nature })
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.ItemCode, e.Descn, e.Nature })
                    .HasName("UK_ItemNatureDesc")
                    .IsUnique();

                entity.Property(e => e.ItemCode).IsUnicode(false);

                entity.Property(e => e.Nature).IsUnicode(false);

                entity.Property(e => e.ContCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.Descn).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.TmItemnature)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemNatureItemCode");
            });

            modelBuilder.Entity<TmItemtype>(entity =>
            {
                entity.HasKey(e => e.ItemType)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Descn)
                    .HasName("UK_ItemTypeDesc")
                    .IsUnique();

                entity.HasIndex(e => e.FrtInAc)
                    .HasName("FK_ItemTypeFrtInAc");

                entity.HasIndex(e => e.FrtOutAc)
                    .HasName("FK_ItemTypeFrtOutAc");

                entity.HasIndex(e => e.OthDedAc)
                    .HasName("FK_ItemTypeOthDedAc");

                entity.HasIndex(e => e.PackingAc)
                    .HasName("FK_ItemTypePackingAc");

                entity.HasIndex(e => e.PurAcOs)
                    .HasName("FK_ItemTypePurAcOs");

                entity.HasIndex(e => e.PurAcWs)
                    .HasName("FK_ItemTypePurAcWs");

                entity.HasIndex(e => e.RoundOff)
                    .HasName("FK_ItemTypeRoundOff");

                entity.HasIndex(e => e.SaleAcOs)
                    .HasName("FK_ItemTypeSaleAcOs");

                entity.HasIndex(e => e.SaleAcWs)
                    .HasName("FK_ItemTypeSaleAcWs");

                entity.HasIndex(e => e.StInAc)
                    .HasName("FK_ItemTypeStInAc");

                entity.HasIndex(e => e.StOutAc)
                    .HasName("FK_ItemTypeStInAcStOutAc");

                entity.Property(e => e.ItemType).IsUnicode(false);

                entity.Property(e => e.CessAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.DamgUnAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.Descn).IsUnicode(false);

                entity.Property(e => e.ExgUnAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.FrtInAc).IsUnicode(false);

                entity.Property(e => e.FrtOutAc).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Nature)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'R'");

                entity.Property(e => e.NetWtCutAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.OthDedAc).IsUnicode(false);

                entity.Property(e => e.PackingAc).IsUnicode(false);

                entity.Property(e => e.PurAcOs)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.PurAcWs)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.QltyCutAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.QtyCutAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.RateCutAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.RoundOff).IsUnicode(false);

                entity.Property(e => e.SaleAcOs)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.SaleAcWs)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StInAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StOutAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StOutDivAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StinDivAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.UnLoadAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.WmtAc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.HasOne(d => d.FrtInAcNavigation)
                    .WithMany(p => p.TmItemtypeFrtInAcNavigation)
                    .HasForeignKey(d => d.FrtInAc)
                    .HasConstraintName("FK_ItemTypeFrtInAc");

                entity.HasOne(d => d.FrtOutAcNavigation)
                    .WithMany(p => p.TmItemtypeFrtOutAcNavigation)
                    .HasForeignKey(d => d.FrtOutAc)
                    .HasConstraintName("FK_ItemTypeFrtOutAc");

                entity.HasOne(d => d.OthDedAcNavigation)
                    .WithMany(p => p.TmItemtypeOthDedAcNavigation)
                    .HasForeignKey(d => d.OthDedAc)
                    .HasConstraintName("FK_ItemTypeOthDedAc");

                entity.HasOne(d => d.PackingAcNavigation)
                    .WithMany(p => p.TmItemtypePackingAcNavigation)
                    .HasForeignKey(d => d.PackingAc)
                    .HasConstraintName("FK_ItemTypePackingAc");

                entity.HasOne(d => d.PurAcOsNavigation)
                    .WithMany(p => p.TmItemtypePurAcOsNavigation)
                    .HasForeignKey(d => d.PurAcOs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypePurAcOs");

                entity.HasOne(d => d.PurAcWsNavigation)
                    .WithMany(p => p.TmItemtypePurAcWsNavigation)
                    .HasForeignKey(d => d.PurAcWs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypePurAcWs");

                entity.HasOne(d => d.RoundOffNavigation)
                    .WithMany(p => p.TmItemtypeRoundOffNavigation)
                    .HasForeignKey(d => d.RoundOff)
                    .HasConstraintName("FK_ItemTypeRoundOff");

                entity.HasOne(d => d.SaleAcOsNavigation)
                    .WithMany(p => p.TmItemtypeSaleAcOsNavigation)
                    .HasForeignKey(d => d.SaleAcOs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypeSaleAcOs");

                entity.HasOne(d => d.SaleAcWsNavigation)
                    .WithMany(p => p.TmItemtypeSaleAcWsNavigation)
                    .HasForeignKey(d => d.SaleAcWs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypeSaleAcWs");

                entity.HasOne(d => d.StInAcNavigation)
                    .WithMany(p => p.TmItemtypeStInAcNavigation)
                    .HasForeignKey(d => d.StInAc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypeStInAc");

                entity.HasOne(d => d.StOutAcNavigation)
                    .WithMany(p => p.TmItemtypeStOutAcNavigation)
                    .HasForeignKey(d => d.StOutAc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemTypeStInAcStOutAc");
            });

            modelBuilder.Entity<TmMeats>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.GradeSection).IsUnicode(false);

                entity.Property(e => e.IsActive).IsUnicode(false);

                entity.Property(e => e.MeatsCode).IsUnicode(false);

                entity.Property(e => e.MeatsName).IsUnicode(false);

                entity.Property(e => e.Section).IsUnicode(false);

                entity.Property(e => e.SectionName).IsUnicode(false);

                entity.Property(e => e.Uom).IsUnicode(false);
            });

            modelBuilder.Entity<TmMenuitem>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.Div1, e.MenuId })
                    .HasName("UK_MenuItemDiv")
                    .IsUnique();

                entity.HasIndex(e => new { e.GroupId, e.MenuId })
                    .HasName("UK_MenuItemGroupId")
                    .IsUnique();

                entity.HasIndex(e => new { e.IsActive, e.MenuId })
                    .HasName("FK_MenuItemIsActiveE")
                    .IsUnique();

                entity.HasIndex(e => new { e.MenuCaption, e.MenuId })
                    .HasName("UK_MenuItemCaption")
                    .IsUnique();

                entity.HasIndex(e => new { e.MenuType, e.MenuId })
                    .HasName("UK_MenuItemType")
                    .IsUnique();

                entity.HasIndex(e => new { e.ModuleCode, e.MenuId })
                    .HasName("UK_MenuItemModule")
                    .IsUnique();

                entity.HasIndex(e => new { e.UnitType, e.MenuId })
                    .HasName("UK_MenuItemUnitType")
                    .IsUnique();

                entity.Property(e => e.MenuId).IsUnicode(false);

                entity.Property(e => e.AcItemType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.AcPartYgc)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.AccChk)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ActionForm).IsUnicode(false);

                entity.Property(e => e.AllocTrType).IsUnicode(false);

                entity.Property(e => e.BcondType).IsUnicode(false);

                entity.Property(e => e.CashSplit)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ColumnName).IsUnicode(false);

                entity.Property(e => e.CondRowBuild)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.DelAllow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.DelStatus)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Div1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'G'");

                entity.Property(e => e.DocPrefix).IsUnicode(false);

                entity.Property(e => e.DocTypeCheck)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.DocVal)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.EditAllow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.EditNotAllow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'I'");

                entity.Property(e => e.ExpCodeTable)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.FormId).IsUnicode(false);

                entity.Property(e => e.GroupId).IsUnicode(false);

                entity.Property(e => e.HstFrom)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'B'");

                entity.Property(e => e.ImpRep)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.IsExp)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.IsRoBr)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ItemType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.MenuCaption).IsUnicode(false);

                entity.Property(e => e.MenuType).IsUnicode(false);

                entity.Property(e => e.MenuUrlL).IsUnicode(false);

                entity.Property(e => e.MisLevels).IsUnicode(false);

                entity.Property(e => e.ModuleCode).IsUnicode(false);

                entity.Property(e => e.ModuleType).IsUnicode(false);

                entity.Property(e => e.Mselect)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.OldPrefix).IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.PpSize)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'V'");

                entity.Property(e => e.PrintNotAllow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'I'");

                entity.Property(e => e.ReportCpi).HasDefaultValueSql("'15'");

                entity.Property(e => e.RoDocPrefix)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.RptOption).IsUnicode(false);

                entity.Property(e => e.SaveConfirm)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StateCheck)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.StkChk)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.Property(e => e.TransferId)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.UnitType).IsUnicode(false);

                entity.Property(e => e.Ver).HasDefaultValueSql("'0'");

                entity.Property(e => e.WfCaption).IsUnicode(false);

                entity.Property(e => e.WfDocType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.WfUrl).IsUnicode(false);

                entity.Property(e => e.WorkFlow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");
            });

            modelBuilder.Entity<TmPartytype>(entity =>
            {
                entity.HasKey(e => e.PartyType)
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.PartyType, e.Descn })
                    .HasName("UK_PartyTypeDesc")
                    .IsUnique();

                entity.Property(e => e.PartyType).IsUnicode(false);

                entity.Property(e => e.Descn).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<TmPlace>(entity =>
            {
                entity.HasKey(e => e.PlaceCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CityCode)
                    .HasName("FK_PlaceCityCode");

                entity.HasIndex(e => e.CountryCode)
                    .HasName("FK_PlaceCountryCode");

                entity.HasIndex(e => e.Currency)
                    .HasName("FK_PlaceCurrency");

                entity.HasIndex(e => e.DistCode)
                    .HasName("FK_PlaceDistCode");

                entity.HasIndex(e => e.PinCode)
                    .HasName("FK_PlacePinCode");

                entity.HasIndex(e => e.PlaceName)
                    .HasName("UK_PlaceName")
                    .IsUnique();

                entity.HasIndex(e => e.StateCode)
                    .HasName("FK_PlaceStateCode");

                entity.HasIndex(e => e.TalukCode)
                    .HasName("FK_PlaceTalukCode");

                entity.HasIndex(e => new { e.PlaceCode, e.Currency })
                    .HasName("UK_PlaceCurr")
                    .IsUnique();

                entity.HasIndex(e => new { e.PlaceType, e.PlaceCode })
                    .HasName("UK_PlacePlaceCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.PlaceType, e.PlaceCode, e.PlaceName, e.CountryCode, e.StateCode, e.DistCode, e.TalukCode, e.CityCode })
                    .HasName("UK_PlacePlaceCodeName")
                    .IsUnique();

                entity.Property(e => e.PlaceCode).IsUnicode(false);

                entity.Property(e => e.CityCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEC'");

                entity.Property(e => e.CityName).IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEO'");

                entity.Property(e => e.CountryName).IsUnicode(false);

                entity.Property(e => e.Currency)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.DistCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONED'");

                entity.Property(e => e.DistName).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.PinCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEZ'");

                entity.Property(e => e.PlaceName).IsUnicode(false);

                entity.Property(e => e.PlaceType).IsUnicode(false);

                entity.Property(e => e.PostOff).IsUnicode(false);

                entity.Property(e => e.SlNo).HasDefaultValueSql("'0'");

                entity.Property(e => e.StateCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONES'");

                entity.Property(e => e.StateName).IsUnicode(false);

                entity.Property(e => e.StdCode).IsUnicode(false);

                entity.Property(e => e.TalukCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONEM'");

                entity.Property(e => e.TalukName).IsUnicode(false);

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.InverseCityCodeNavigation)
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceCityCode");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.InverseCountryCodeNavigation)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceCountryCode");

                entity.HasOne(d => d.CurrencyNavigation)
                    .WithMany(p => p.TmPlace)
                    .HasForeignKey(d => d.Currency)
                    .HasConstraintName("FK_PlaceCurrency");

                entity.HasOne(d => d.DistCodeNavigation)
                    .WithMany(p => p.InverseDistCodeNavigation)
                    .HasForeignKey(d => d.DistCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceDistCode");

                entity.HasOne(d => d.PinCodeNavigation)
                    .WithMany(p => p.InversePinCodeNavigation)
                    .HasForeignKey(d => d.PinCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlacePinCode");

                entity.HasOne(d => d.StateCodeNavigation)
                    .WithMany(p => p.InverseStateCodeNavigation)
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceStateCode");

                entity.HasOne(d => d.TalukCodeNavigation)
                    .WithMany(p => p.InverseTalukCodeNavigation)
                    .HasForeignKey(d => d.TalukCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaceTalukCode");
            });

            modelBuilder.Entity<TmProdnature>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<TmReason>(entity =>
            {
                entity.HasKey(e => new { e.BranchCode, e.DocType, e.ReasonCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => new { e.BranchCode, e.DocType, e.ReasonName })
                    .HasName("UK_Reason")
                    .IsUnique();

                entity.HasIndex(e => new { e.ReasonName, e.BranchCode, e.DocType })
                    .HasName("UK_ReasonBrDocTpReasonName")
                    .IsUnique();

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.DocType).IsUnicode(false);

                entity.Property(e => e.ReasonCode).IsUnicode(false);

                entity.Property(e => e.AccountCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.GrpCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.ReasonName).IsUnicode(false);

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmReason)
                    .HasForeignKey(d => d.BranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReasonBranch");
            });

            modelBuilder.Entity<TmRegion>(entity =>
            {
                entity.HasKey(e => e.RegionCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RegHeadCode)
                    .HasName("RegionHead");

                entity.HasIndex(e => e.RobrCode)
                    .HasName("RegionRoBr");

                entity.HasIndex(e => new { e.RegZoneCode, e.RegionName, e.RegionCode })
                    .HasName("RegionRegZoneRegion")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegionCode, e.RegionName, e.IsActive, e.RegZoneCode })
                    .HasName("RegionRegName")
                    .IsUnique();

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''A'''");

                entity.Property(e => e.RegHeadCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'00724'");

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.Property(e => e.RegionName).IsUnicode(false);

                entity.Property(e => e.RobrCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''NONE'''");

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.SlNo).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.RegHeadCodeNavigation)
                    .WithMany(p => p.TmRegion)
                    .HasForeignKey(d => d.RegHeadCode)
                    .HasConstraintName("RegionHead");

                entity.HasOne(d => d.RegZoneCodeNavigation)
                    .WithMany(p => p.TmRegion)
                    .HasForeignKey(d => d.RegZoneCode)
                    .HasConstraintName("RegionRegZone");

                entity.HasOne(d => d.RobrCodeNavigation)
                    .WithMany(p => p.TmRegion)
                    .HasForeignKey(d => d.RobrCode)
                    .HasConstraintName("RegionRoBr");
            });

            modelBuilder.Entity<TmRegionmap>(entity =>
            {
                entity.HasKey(e => new { e.RegionCode, e.DivCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CompanyCode)
                    .HasName("FK_RegionMapCompany");

                entity.HasIndex(e => e.DivCode)
                    .HasName("FK_RegionMapDiv");

                entity.HasIndex(e => e.RegZoneCode)
                    .HasName("FK_RegionMapRz");

                entity.HasIndex(e => new { e.RegionCode, e.DivCode, e.CompanyCode, e.RegZoneCode, e.IsActive })
                    .HasName("UK_RegionMap")
                    .IsUnique();

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.DivCode).IsUnicode(false);

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.TmRegionmap)
                    .HasForeignKey(d => d.CompanyCode)
                    .HasConstraintName("FK_RegionMapCompany");

                entity.HasOne(d => d.DivCodeNavigation)
                    .WithMany(p => p.TmRegionmap)
                    .HasForeignKey(d => d.DivCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegionMapDiv");

                entity.HasOne(d => d.RegZoneCodeNavigation)
                    .WithMany(p => p.TmRegionmap)
                    .HasForeignKey(d => d.RegZoneCode)
                    .HasConstraintName("FK_RegionMapRz");

                entity.HasOne(d => d.RegionCodeNavigation)
                    .WithMany(p => p.TmRegionmap)
                    .HasForeignKey(d => d.RegionCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegionMapReg");
            });

            modelBuilder.Entity<TmRegzone>(entity =>
            {
                entity.HasKey(e => e.RegZoneCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RegZoneName)
                    .HasName("RegZoneName")
                    .IsUnique();

                entity.HasIndex(e => e.ShortName)
                    .HasName("RegZoneShortName")
                    .IsUnique();

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.Property(e => e.BcOnIdPfix)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.BconSysId).HasDefaultValueSql("'724'");

                entity.Property(e => e.BeAcOnLc).IsUnicode(false);

                entity.Property(e => e.BeAcOnZc).IsUnicode(false);

                entity.Property(e => e.CmpId).IsUnicode(false);

                entity.Property(e => e.HrHead)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'00724'");

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'''A'''");

                entity.Property(e => e.RegZoneAcHead)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'00724'");

                entity.Property(e => e.RegZoneBr).IsUnicode(false);

                entity.Property(e => e.RegZoneHead)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'00724'");

                entity.Property(e => e.RegZoneName).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);
            });

            modelBuilder.Entity<TmRegzonemap>(entity =>
            {
                entity.HasKey(e => new { e.RegZoneCode, e.CompanyCode, e.IsActive, e.DivCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.CompanyCode)
                    .HasName("FK_RegZoneMapCom");

                entity.HasIndex(e => e.DivCode)
                    .HasName("FK_RegZoneMapDiv");

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.DivCode).IsUnicode(false);

                entity.HasOne(d => d.CompanyCodeNavigation)
                    .WithMany(p => p.TmRegzonemap)
                    .HasForeignKey(d => d.CompanyCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegZoneMapCom");

                entity.HasOne(d => d.DivCodeNavigation)
                    .WithMany(p => p.TmRegzonemap)
                    .HasForeignKey(d => d.DivCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegZoneMapDiv");

                entity.HasOne(d => d.RegZoneCodeNavigation)
                    .WithMany(p => p.TmRegzonemap)
                    .HasForeignKey(d => d.RegZoneCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegZoneMapRz");
            });

            modelBuilder.Entity<TmTdsac>(entity =>
            {
                entity.HasKey(e => new { e.AccountCode, e.Nature, e.IsActive, e.DocIntNo })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Nature)
                    .HasName("FK_TdsAcNature");

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.Nature).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.DocNo)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.NatureNavigation)
                    .WithMany(p => p.TmTdsac)
                    .HasForeignKey(d => d.Nature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TdsAcNature");
            });

            modelBuilder.Entity<TmTdsnature>(entity =>
            {
                entity.HasKey(e => e.NatureCode)
                    .HasName("PRIMARY");

                entity.Property(e => e.NatureCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.NatureDesc).IsUnicode(false);
            });

            modelBuilder.Entity<TmUom>(entity =>
            {
                entity.HasKey(e => e.Uom)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.UomName)
                    .HasName("UK_UomName")
                    .IsUnique();

                entity.Property(e => e.Uom).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.UomName).IsUnicode(false);
            });

            modelBuilder.Entity<TmUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ReigionCode)
                    .HasName("TmUserRegCode");

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.AddRights).IsUnicode(false);

                entity.Property(e => e.AdminUser)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.Category)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'BR'");

                entity.Property(e => e.DocType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.EliteUser)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.InteCode).IsUnicode(false);

                entity.Property(e => e.Logged)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.MailId).IsUnicode(false);

                entity.Property(e => e.MisRights)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.PassWord).IsUnicode(false);

                entity.Property(e => e.PassWordCheckMode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'E'");

                entity.Property(e => e.PassWordValidIpTo).HasDefaultValueSql("'7'");

                entity.Property(e => e.ReigionCode).IsUnicode(false);

                entity.Property(e => e.SessionId).IsUnicode(false);

                entity.HasOne(d => d.ReigionCodeNavigation)
                    .WithMany(p => p.TmUser)
                    .HasForeignKey(d => d.ReigionCode)
                    .HasConstraintName("TmUserRegCode");
            });

            modelBuilder.Entity<TmUserdefault>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("UserDefaultBr");

                entity.HasIndex(e => e.DivisionCode)
                    .HasName("UserDefaultDiv");

                entity.HasIndex(e => e.RegZoneCode)
                    .HasName("UserDefaultRegZ");

                entity.HasIndex(e => e.RegionCode)
                    .HasName("UserDefaultReg");

                entity.HasIndex(e => new { e.UserId, e.BranchCode, e.BranchState })
                    .HasName("UserDefUserIdBrState")
                    .IsUnique();

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.BranchName).IsUnicode(false);

                entity.Property(e => e.BranchState).IsUnicode(false);

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.DivisionCode).IsUnicode(false);

                entity.Property(e => e.DivisionName).IsUnicode(false);

                entity.Property(e => e.Logged).IsUnicode(false);

                entity.Property(e => e.RegZoneCode).IsUnicode(false);

                entity.Property(e => e.RegZoneName).IsUnicode(false);

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.SessionId).IsUnicode(false);

                entity.Property(e => e.Upassword).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.ZoneCode).IsUnicode(false);

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmUserdefault)
                    .HasForeignKey(d => d.BranchCode)
                    .HasConstraintName("UserDefaultBr");

                entity.HasOne(d => d.DivisionCodeNavigation)
                    .WithMany(p => p.TmUserdefault)
                    .HasForeignKey(d => d.DivisionCode)
                    .HasConstraintName("UserDefaultDiv");

                entity.HasOne(d => d.RegZoneCodeNavigation)
                    .WithMany(p => p.TmUserdefault)
                    .HasForeignKey(d => d.RegZoneCode)
                    .HasConstraintName("UserDefaultRegZ");

                entity.HasOne(d => d.RegionCodeNavigation)
                    .WithMany(p => p.TmUserdefault)
                    .HasForeignKey(d => d.RegionCode)
                    .HasConstraintName("UserDefaultReg");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TmUserdefault)
                    .HasForeignKey<TmUserdefault>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserDefaultEmp");

                entity.HasOne(d => d.UserNavigation)
                    .WithOne(p => p.TmUserdefault)
                    .HasForeignKey<TmUserdefault>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserDefaultUser");
            });

            modelBuilder.Entity<TmUserdivmap>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DivCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.DivCode)
                    .HasName("FK_UserDivMapDiv");

                entity.HasIndex(e => new { e.IsActive, e.UserId, e.DivCode })
                    .HasName("UK_UserDivMapIsActive")
                    .IsUnique();

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.DivCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.DivCodeNavigation)
                    .WithMany(p => p.TmUserdivmap)
                    .HasForeignKey(d => d.DivCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDivMapDiv");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TmUserdivmap)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDivMapEmp");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.TmUserdivmap)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDivMapUser");
            });

            modelBuilder.Entity<TmUserright>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BranchCode, e.MenuId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.BranchCode)
                    .HasName("UserRightBR");

                entity.HasIndex(e => e.UnitType)
                    .HasName("UserRightUnitType");

                entity.Property(e => e.UserId).IsUnicode(false);

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.MenuId).IsUnicode(false);

                entity.Property(e => e.RowFor).IsUnicode(false);

                entity.Property(e => e.Udelete)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Uedit)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UnitType).IsUnicode(false);

                entity.Property(e => e.Uprint)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Usave)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Uview)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UworkFlow)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.BranchCodeNavigation)
                    .WithMany(p => p.TmUserright)
                    .HasForeignKey(d => d.BranchCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRightBR");

                entity.HasOne(d => d.UnitTypeNavigation)
                    .WithMany(p => p.TmUserright)
                    .HasForeignKey(d => d.UnitType)
                    .HasConstraintName("UserRightUnitType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TmUserright)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRightTemp");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.TmUserright)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRightUserId");
            });

            modelBuilder.Entity<TmVendor>(entity =>
            {
                entity.HasKey(e => new { e.AccountCode, e.ItemCode })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ItemCode)
                    .HasName("FK_VendorItem");

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.ItemCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.PurType)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NP'");

                entity.HasOne(d => d.AccountCodeNavigation)
                    .WithMany(p => p.TmVendor)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendorAc");

                entity.HasOne(d => d.AccountCode1)
                    .WithMany(p => p.TmVendor)
                    .HasForeignKey(d => d.AccountCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendorAcDtl");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.TmVendor)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendorItem");
            });

            modelBuilder.Entity<TmZone>(entity =>
            {
                entity.HasKey(e => e.ZoneCode)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RegZoneCode)
                    .HasName("ZoneRegZones");

                entity.HasIndex(e => e.ZoneBrCode)
                    .HasName("ZoneBr");

                entity.HasIndex(e => e.ZoneHeadCode)
                    .HasName("ZoneHead");

                entity.HasIndex(e => e.ZoneName)
                    .HasName("ZoneZoneName")
                    .IsUnique();

                entity.HasIndex(e => new { e.RegionCode, e.ZoneCode })
                    .HasName("ZoneRegZone")
                    .IsUnique();

                entity.HasIndex(e => new { e.ZoneCode, e.ZoneName, e.RegionCode, e.IsActive, e.RegZoneCode })
                    .HasName("ZoneZoneRegZ")
                    .IsUnique();

                entity.Property(e => e.ZoneCode).IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.RegZoneCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);

                entity.Property(e => e.ZoneBrCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.ZoneHeadCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-00724'");

                entity.Property(e => e.ZoneName).IsUnicode(false);

                entity.HasOne(d => d.RegZoneCodeNavigation)
                    .WithMany(p => p.TmZone)
                    .HasForeignKey(d => d.RegZoneCode)
                    .HasConstraintName("ZoneRegZones");

                entity.HasOne(d => d.RegionCodeNavigation)
                    .WithMany(p => p.TmZone)
                    .HasForeignKey(d => d.RegionCode)
                    .HasConstraintName("ZoneRegCode");

                entity.HasOne(d => d.ZoneBrCodeNavigation)
                    .WithMany(p => p.TmZone)
                    .HasForeignKey(d => d.ZoneBrCode)
                    .HasConstraintName("ZoneBr");

                entity.HasOne(d => d.ZoneHeadCodeNavigation)
                    .WithMany(p => p.TmZone)
                    .HasForeignKey(d => d.ZoneHeadCode)
                    .HasConstraintName("ZoneHead");
            });

            modelBuilder.Entity<TtShedready>(entity =>
            {
                entity.HasKey(e => e.DocIntNo)
                    .HasName("PRIMARY");

                entity.Property(e => e.BranchCode).IsUnicode(false);

                entity.Property(e => e.CompanyCode).IsUnicode(false);

                entity.Property(e => e.DcQty).HasDefaultValueSql("'0'");

                entity.Property(e => e.DocDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DocNo).IsUnicode(false);

                entity.Property(e => e.FarmCode).IsUnicode(false);

                entity.Property(e => e.LineCode).IsUnicode(false);

                entity.Property(e => e.MaleRatio).HasDefaultValueSql("'0'");

                entity.Property(e => e.OrderQty).HasDefaultValueSql("'0'");

                entity.Property(e => e.RefDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RefNo).IsUnicode(false);

                entity.Property(e => e.RegZoneCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'APZONE'");

                entity.Property(e => e.RegionCode).IsUnicode(false);

                entity.Property(e => e.ScheduleQty).HasDefaultValueSql("'0'");

                entity.Property(e => e.SopStatus)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.SubBrCode)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NONE'");

                entity.Property(e => e.Supervisor).IsUnicode(false);

                entity.Property(e => e.ZoneCode).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
