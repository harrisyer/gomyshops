using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GoMyShops.Data.Entity;
//using GoMyShops.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GoMyShops.Models;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoMyShops.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        //public DataContext()
        //{
        //}
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<SignUp> SignUps { get; set; }
      
        public DbSet<ModuleSU> ModuleSU { get; set; }
        public DbSet<LoginSU> LoginSU { get; set; }
        public DbSet<AccountManager> AccountManagers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<StatusSU> StatusSU { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<DocSequenceSU> DocSequenceSU { get; set; }
        public DbSet<UserAccessLevel> UserAccessLevels { get; set; }
        public DbSet<UserGroupAccess> UserGroupAccesses { get; set; }
        public DbSet<ModuleActionSU> ModuleActionSU { get; set; }
        public DbSet<AppCtrlSU> AppCtrlSU { get; set; }
        public DbSet<AppCtrlUserProfile> AppCtrlUserProfiles { get; set; }
        public DbSet<AuditMaster> AuditMasters { get; set; }
        public DbSet<AuditDetail> AuditDetails { get; set; }
        public DbSet<EditDetail> EditDetails { get; set; }
       
        public DbSet<LookupIndustrySU> LookupIndustrySU { get; set; }
        public DbSet<MccCodeSU> MccCodeSU { get; set; }
      
        public DbSet<ErrorCodeSU> ErrorCodeSU { get; set; }
        //public DbSet<Currency> Currencys { get; set; }
        public DbSet<Customer> Customers { get; set; }
       
        public DbSet<Partner> Partners { get; set; }
      
        public DbSet<SysParameterSU> SysParameterSU { get; set; }
        public DbSet<IntegrationSU> IntegrationSU { get; set; }
        public DbSet<RiskScoreSU> RiskScoreSU { get; set; }
       
        public DbSet<EmailMaster> EmailMasters { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<RecipientGroup> RecipientGroups { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<RecipientConnection> RecipientConnections { get; set; }
        public DbSet<RecipientConnectionDetail> RecipientConnectionDetails { get; set; }
        //public DbSet<RecipientConnectionTransaction> RecipientConnectionTransactions { get; set; }
        //public DbSet<RecipientConnectionTransactionFile> RecipientConnectionTransactionFiles { get; set; }
        public DbSet<Documents> Documents { get; set; }
        //public DbSet<CommentChat> CommentChats { get; set; }
        //public DbSet<TransactionRecordSummary> TransactionRecordSummary { get; set; }
        //public DbSet<TransactionRecordSummaryDetail> TransactionRecordSummaryDetail { get; set; }
        //public DbSet<FundingLedgerDetails> FundingLedgerDetails { get; set; }
        //public DbSet<FundingLedgerAdjustment> FundingLedgerAdjustment { get; set; }
        //public DbSet<TaxInvoice> TaxInvoice { get; set; }
        //public DbSet<TaxInvoiceDetail> TaxInvoiceDetail { get; set; }
        //public DbSet<TaxInvoiceDetailSOA> TaxInvoiceDetailSOA { get; set; }
        public DbSet<Bank> Bank { get; set; }
        //public DbSet<StatementOfAccount> StatementOfAccount { get; set; }
        //public DbSet<StatementOfAccountDetail> StatementOfAccountDetail { get; set; }
        //public DbSet<ProcessorCardBinRule> ProcessorCardBinRule { get; set; }
        //public DbSet<ProcessorCardType> ProcessorCardType { get; set; }
        //public DbSet<ProcessorCurrency> ProcessorCurrency { get; set; }
        //public DbSet<ProcessorMCC> ProcessorMCC { get; set; }
        //public DbSet<ProcessorPriorityBin> ProcessorPriorityBin { get; set; }
        public DbSet<ApplicationLog> ApplicationLog { get; set; }
        //public DbSet<FWUIntegrationLog> FWUIntegrationLog { get; set; }
        //public DbSet<MIDRiskAlertUser> MIDRiskAlertUser { get; set; }
        //public DbSet<TransactionChargebackRetrieval> TransactionChargebackRetrieval { get; set; }
        public DbSet<ReasonSU> ReasonSU { get; set; }
        //public DbSet<TransactionVelocityLog> TransactionVelocityLog { get; set; }
        //public DbSet<WireFee> WireFees { get; set; }
        //public DbSet<LinkPaymentTransaction> LinkPaymentTransaction { get; set; }
        //public DbSet<LinkPaymentItem> LinkPaymentItem { get; set; }
        //public DbSet<PartnerFunding> PartnerFundings { get; set; }
        //public DbSet<PartnerLedger> PartnerLedgers { get; set; }
        //public DbSet<PartnerLedgerDetails> PartnerLedgerDetails { get; set; }
        //public DbSet<OtherFee> OtherFees { get; set; }
        //public DbSet<LinkPaymentLog> LinkPaymentLog { get; set; }
        //public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<Param> Params { get; set; }
        public DbSet<ParamSU> ParamSU { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

          //optionsBuilder.UseSqlServer(@"data source=123; initial catalog=abc;user id=admin;password=123;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);

         //   modelBuilder.Entity<AspNetUserLogin>()
         //.HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

         //   modelBuilder.Entity<AspNetUser>(b =>
         //   {
         //       // The relationships between User and other entity types
         //       // Note that these relationships are configured with no navigation properties

         //       //// Each User can have many UserClaims
         //       //b.HasMany<AspNetUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

         //       //// Each User can have many UserLogins
         //       //b.HasMany<AspNetUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

         //       //// Each User can have many UserTokens
         //       //b.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

         //       //Each User can have many entries in the UserRole join table
         //       //b.HasMany<AspNetUserRole>().WithOne().HasForeignKey(ur => ur.Id).IsRequired();
         //   });

            //modelBuilder.Entity<AspNetRole>(b =>
            //{
            //    // The relationships between User and other entity types
            //    // Note that these relationships are configured with no navigation properties

            //    // Each Role can have many entries in the UserRole join table
            //    b.HasMany<AspNetUser>().WithOne().HasForeignKey(ur => ur.).IsRequired();

            //});



            modelBuilder.Entity<ModuleSU>()
           .HasKey(p => new { p.ModuleID, p.ModuleCode });

            modelBuilder.Entity<Param>()
          .HasKey(p => new { p.ParamCode, p.ParamValue });

            modelBuilder.Entity<State>()
        .HasKey(p => new { p.StateCode, p.CountryCode });

            modelBuilder.Entity<AccountManager>()
            .HasIndex(b => b.AccountManagerCode)
            .IsUnique()
            .HasDatabaseName("IX_AccountManager");

            modelBuilder.Entity<Announcement>()
           .HasIndex(p => new { p.StartTime, p.EndTime })
           .HasDatabaseName("IX_Announcement_Time");

            modelBuilder.Entity<Bank>()
           .HasIndex(b => b.BankCode)
           .IsUnique()
           .HasDatabaseName("IX_Bank_BankCode");

            modelBuilder.Entity<Branch>()
           .HasIndex(b => b.BranchCode)
           .HasDatabaseName("IX_Branch");

           modelBuilder.Entity<Customer>()
          .HasIndex(b => b.CustomerCode)
          .IsUnique()
          .HasDatabaseName("IX_Customer_CustomerCode");

            modelBuilder.Entity<Customer>()
         .HasIndex(b => b.PartnerCode)
         .HasDatabaseName("IX_Customer_PartnerCode");

          modelBuilder.Entity<Documents>()
        .HasIndex(b => b.DocumentCode)
        .IsUnique()
        .HasDatabaseName("IX_Document");

            modelBuilder.Entity<EditDetail>()
           .HasIndex(p => new { p.DataModel, p.KeyFieldValue })
           //.IsUnique()
           .HasDatabaseName("IX_EditDetail_DataModelKeyFieldValue");

            modelBuilder.Entity<ErrorCodeSU>()
           .HasIndex(b => b.ErrorCode)
           .IsUnique()
           .HasDatabaseName("IX_ErrorCodeSUErrorCode");    

            modelBuilder.Entity<IntegrationSU>()
           .HasIndex(p => new { p.IntegrationCode, p.IntegrationType })
           .IsUnique()
           .HasDatabaseName("IX_IntegrationSU_IntegrationCode");

            modelBuilder.Entity<Location>()
           .HasIndex(b => b.LocationCode)
           .IsUnique()
           .HasDatabaseName("IX_LocationCode");

            modelBuilder.Entity<Location>()
           .HasIndex(b => b.BranchCode)
           //.IsUnique()
           .HasDatabaseName("IX_Branch");

            modelBuilder.Entity<Partner>()
           .HasIndex(b => b.PartnerCode)
           .IsUnique()
           .HasDatabaseName("IX_Partner_PartnerCode");

            modelBuilder.Entity<Partner>()
            .HasIndex(b => b.PartnerName)
            .IsUnique()
            .HasDatabaseName("IX_Partner_PartnerName");

            modelBuilder.Entity<Recipient>()
           .HasIndex(b => b.RecipientCode)
           .IsUnique()
           .HasDatabaseName("IX_Recipient");

            modelBuilder.Entity<RecipientConnection>()
           .HasIndex(b => b.RecipientConnectionCode)
           .IsUnique()
           .HasDatabaseName("IX_RecipientConnection");

            modelBuilder.Entity<RecipientGroup>()
           .HasIndex(b => b.RecipientGroupCode)
           .IsUnique()
           .HasDatabaseName("IX_RecipientGroupCode");

            modelBuilder.Entity<SysParameterSU>()
           .HasIndex(b => b.ParamCode)
           .IsUnique()
           .HasDatabaseName("IX_SysParameterSUParamCode");         
           
            modelBuilder.Entity<User>()
            .HasIndex(b => b.CustomerCode)
            //.IsUnique()
            .HasDatabaseName("IX_User_CustomerCode");

            modelBuilder.Entity<UserGroup>()
           .HasIndex(p => new { p.CompanyCode, p.GroupCode, p.CustomerCode })
           .IsUnique()
           .HasDatabaseName("UK_UserGroup_GroupCode");

            // Harris (Test) Modify Core

            //This will singularize all table names
            //foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    entityType.SetTableName(entityType.Name) ;
            //}
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ParamSU>()
                .HasMany(e => e.iParams)
                .WithOne(e => e.iParamSu)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditDetail>()
            .Property(b => b.CompositeKeys)
            .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<AuditDetail>()
            .Property(b => b.ValueChanges)
            .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<AuditDetail>()
            .Property(b => b.ValueBefore)
            .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<AuditDetail>()
            .Property(b => b.ValueAfter)
            .HasColumnType("nvarchar(max)");         

           modelBuilder.Entity<SysParameterSU>()
            .Property(b => b.ParamValue)
            .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<ApplicationLog>()
           .Property(b => b.Message)
           .HasColumnType("nvarchar(max)");

            //Need to be at here cause else will using namepace plus name to create table.

           // modelBuilder.Entity<AspNetUser>().ToTable("AspNetUsers");
           // modelBuilder.Entity<AspNetUserLogin>().ToTable("AspNetUserLogins");
           // modelBuilder.Entity<AspNetRole>().ToTable("AspNetRoles");
           // modelBuilder.Entity<AspNetUserClaim>().ToTable("AspNetUserClaims");
          

           // modelBuilder.Entity<SignUp>().ToTable("SignUp");           
           // modelBuilder.Entity<ModuleSU>().ToTable("ModuleSU");
           // modelBuilder.Entity<LoginSU>().ToTable("LoginSU");
           // modelBuilder.Entity<AccountManager>().ToTable("AccountManager");
           // modelBuilder.Entity<Announcement>().ToTable("Announcement");
           // modelBuilder.Entity<UserGroup>().ToTable("UserGroup");
           // modelBuilder.Entity<StatusSU>().ToTable("StatusSU");
           // modelBuilder.Entity<Branch>().ToTable("Branch");
           // modelBuilder.Entity<Company>().ToTable("Company");
           // modelBuilder.Entity<Distributor>().ToTable("Distributor");
           // modelBuilder.Entity<User>().ToTable("User");
           // modelBuilder.Entity<Country>().ToTable("Country");
           // modelBuilder.Entity<State>().ToTable("State");
           // modelBuilder.Entity<DocSequenceSU>().ToTable("DocSequenceSU");
           // modelBuilder.Entity<UserAccessLevel>().ToTable("UserAccessLevel");
           // modelBuilder.Entity<UserGroupAccess>().ToTable("UserGroupAccess");
           // modelBuilder.Entity<ModuleActionSU>().ToTable("ModuleActionSU");
           // modelBuilder.Entity<AppCtrlSU>().ToTable("AppCtrlSU");
           // modelBuilder.Entity<AppCtrlUserProfile>().ToTable("AppCtrlUserProfile");
           // modelBuilder.Entity<AuditMaster>().ToTable("AuditMaster");
           // modelBuilder.Entity<AuditDetail>().ToTable("AuditDetail");
           // modelBuilder.Entity<EditDetail>().ToTable("EditDetail");
           // modelBuilder.Entity<LookupIndustrySU>().ToTable("LookupIndustrySU");
           // modelBuilder.Entity<MccCodeSU>().ToTable("MccCodeSU");          
           // modelBuilder.Entity<ErrorCodeSU>().ToTable("ErrorCodeSU");           
           // modelBuilder.Entity<Customer>().ToTable("Customer");          
           // modelBuilder.Entity<Partner>().ToTable("Partner");          
           // modelBuilder.Entity<SysParameterSU>().ToTable("SysParameterSU");
           // modelBuilder.Entity<IntegrationSU>().ToTable("IntegrationSU");           
           // modelBuilder.Entity<RiskScoreSU>().ToTable("RiskScoreSU");            
           // modelBuilder.Entity<EmailMaster>().ToTable("EmailMaster");
           // modelBuilder.Entity<Email>().ToTable("Email");
           // modelBuilder.Entity<RecipientGroup>().ToTable("RecipientGroup");
           // modelBuilder.Entity<Recipient>().ToTable("Recipient");
           // modelBuilder.Entity<RecipientConnection>().ToTable("RecipientConnection");
           // modelBuilder.Entity<RecipientConnectionDetail>().ToTable("RecipientConnectionDetail");
           //modelBuilder.Entity<Documents>().ToTable("Document");           
           // modelBuilder.Entity<Bank>().ToTable("Bank");           
           // modelBuilder.Entity<ReasonSU>().ToTable("ReasonSU");            
           // modelBuilder.Entity<Param>().ToTable("Param");
           // modelBuilder.Entity<ParamSU>().ToTable("ParamSU");
            modelBuilder.Entity<BoardGames_Domains>()
                .HasKey(i => new { i.BoardGameId, i.DomainId });

            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGames_Domains)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(o => o.Domain)
                .WithMany(m => m.BoardGames_Domains)
                .HasForeignKey(f => f.DomainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasKey(i => new { i.BoardGameId, i.MechanicId });

            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGames_Mechanics)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne(o => o.Mechanic)
                .WithMany(m => m.BoardGames_Mechanics)
                .HasForeignKey(f => f.MechanicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<SYS_Setting> SYS_Settings { get; set; }
        public DbSet<SYS_DataSetting> SYS_DataSettings { get; set; }
        public DbSet<BoardGame> BoardGames => Set<BoardGame>();
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<BoardGames_Domains> BoardGames_Domains => Set<BoardGames_Domains>();
        public DbSet<BoardGames_Mechanics> BoardGames_Mechanics => Set<BoardGames_Mechanics>();   
    }//end class
}//end namespace