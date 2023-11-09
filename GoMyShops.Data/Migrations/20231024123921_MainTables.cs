using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMyShops.Data.Migrations
{
    /// <inheritdoc />
    public partial class MainTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "SettingsType",
                table: "SYS_Settings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SettingValue",
                table: "SYS_Settings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SettingName",
                table: "SYS_Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AccountManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountManagerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountManagerUserCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountManagerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DisplayFrequency = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsMerchant = table.Column<bool>(type: "bit", nullable: false),
                    IsPartner = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCtrlSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppCtrlID = table.Column<int>(type: "int", nullable: false),
                    AppCtrName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppCtrDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AppCtrType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCtrlSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCtrlUserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppCtrlID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCtrlUserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessEvent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsError = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditID = table.Column<int>(type: "int", nullable: false),
                    KeyFieldID = table.Column<int>(type: "int", nullable: false),
                    AuditActionTypeENUM = table.Column<int>(type: "int", nullable: false),
                    KeyFieldValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AreaAccessed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompositeKeys = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueChanges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueAfter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditMasters",
                columns: table => new
                {
                    AuditID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrowserID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BrowserType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditMasters", x => x.AuditID);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DistributorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GSTRegNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GSTRegNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CountryCode2 = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OnboardingCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BusinessEntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    AccountManagerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PartnerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TradingAsDba = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CompanyUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Personnel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TitleOther = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GenderCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailCustomerService = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailFinance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailRisk = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailTechnical = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OfficeNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OfficeExt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactMerchant = table.Column<bool>(type: "bit", nullable: false),
                    ContactReseller = table.Column<bool>(type: "bit", nullable: false),
                    BankAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankAddress1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BankAddress2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BankCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankZip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankCountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    InstitutionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransitNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SwiftNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RateReserve = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RateRefundRatio = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReserveMonth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FeeMonthly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeYearly = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeHighRisk = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeSettle = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeVoid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeRefund = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeDecline = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeRetrieval = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeChargeBack = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeSetup = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeWire = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ApplicationRemark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WhiteList = table.Column<bool>(type: "bit", nullable: false),
                    CreateCheckBy1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateCheckBy1Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateCheckBy2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateCheckBy2Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditCheckBy1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EditCheckBy1Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditCheckBy2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EditCheckBy2Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedApplicationStatusBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedApplicationStatusTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedStatusBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedStatusTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ApproveRemark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DistributorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GSTRegNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocSequenceSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistributorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowsVersion = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocSequenceSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KeyFieldValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompositeKeys = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueChanges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueAfter = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailFrom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EmailTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TokenExpireHour = table.Column<int>(type: "int", nullable: false),
                    CompanyURL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CompanyWebSite = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCodeSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ModelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCodeSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegrationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IntegrationType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IntegrationName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    BusinessModel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsStation = table.Column<bool>(type: "bit", nullable: false),
                    CommissionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginSUs",
                columns: table => new
                {
                    SecurityId = table.Column<int>(type: "int", nullable: false),
                    SecurityName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    AllowOnlyAlphanumericUserNames = table.Column<bool>(type: "bit", nullable: false),
                    RequireUniqueEmail = table.Column<bool>(type: "bit", nullable: false),
                    RequiredLength = table.Column<int>(type: "int", nullable: false),
                    RequireNonLetterOrDigit = table.Column<bool>(type: "bit", nullable: false),
                    RequireDigit = table.Column<bool>(type: "bit", nullable: false),
                    RequireLowercase = table.Column<bool>(type: "bit", nullable: false),
                    RequireUppercase = table.Column<bool>(type: "bit", nullable: false),
                    UserLockoutEnabledByDefault = table.Column<bool>(type: "bit", nullable: false),
                    DefaultAccountLockoutTimeSpan = table.Column<int>(type: "int", nullable: false),
                    MaxFailedAccessAttemptsBeforeLockout = table.Column<int>(type: "int", nullable: false),
                    RequireApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequireChangePasswordInPeriod = table.Column<bool>(type: "bit", nullable: false),
                    ChangePasswordInPeriodTimeSpan = table.Column<int>(type: "int", nullable: false),
                    RequireFirstTimeChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginSUs", x => x.SecurityId);
                });

            migrationBuilder.CreateTable(
                name: "LookupIndustrySU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupIndustrySU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MccCodeSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndustryCode = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MccDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MccCodeSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleActionSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleActionType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    MenuFlag = table.Column<bool>(type: "bit", nullable: false),
                    DetailFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateFlag = table.Column<bool>(type: "bit", nullable: false),
                    EditFlag = table.Column<bool>(type: "bit", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    ApproveFlag = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleActionSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleSU",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    ModuleCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ParentModuleID = table.Column<int>(type: "int", nullable: true),
                    ResourceKey = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModuleDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModuleSequence = table.Column<int>(type: "int", nullable: false),
                    ModuleStatus = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Default = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TargetAction = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TargetController = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApplicationType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleSU", x => new { x.ModuleID, x.ModuleCode });
                });

            migrationBuilder.CreateTable(
                name: "ParamSUs",
                columns: table => new
                {
                    ParamCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ParamCodeDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamSUs", x => x.ParamCode);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PartnerName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PartnerType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    AccountManagerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailCustomerService = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailFinance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailRisk = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailTechnical = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FaxNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BankAccountNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankAddress1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BankAddress2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BankState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BankZip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BankCountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    InstitutionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TransitNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SwiftNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PartnerWireFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReasonSU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReasonCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonSU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientConnectionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientConnectionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientGroupCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientConnectionType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientConnectionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientConnectionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientConnectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientGroupCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientGroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskScoreSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ScoreName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ScoreType = table.Column<int>(type: "int", nullable: false),
                    StartScore = table.Column<int>(type: "int", nullable: false),
                    EndScore = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskScoreSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignUps",
                columns: table => new
                {
                    SignUpName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyRegistrationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignUps", x => x.SignUpName);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => new { x.StateCode, x.CountryCode });
                });

            migrationBuilder.CreateTable(
                name: "StatusSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysParameterSUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParamCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParamValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysParameterSUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DistributorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    MenuFlag = table.Column<bool>(type: "bit", nullable: false),
                    DetailFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreateFlag = table.Column<bool>(type: "bit", nullable: false),
                    EditFlag = table.Column<bool>(type: "bit", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    ApproveFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    GroupType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    SecurityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DistributorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImageCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phrase = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Params",
                columns: table => new
                {
                    ParamCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ParamValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParamDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParamKey = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    ParamStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Params", x => new { x.ParamCode, x.ParamValue });
                    table.ForeignKey(
                        name: "FK_Params_ParamSUs_ParamCode",
                        column: x => x.ParamCode,
                        principalTable: "ParamSUs",
                        principalColumn: "ParamCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountManager",
                table: "AccountManagers",
                column: "AccountManagerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_Time",
                table: "Announcements",
                columns: new[] { "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Bank_BankCode",
                table: "Bank",
                column: "BankCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "Branches",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PartnerCode",
                table: "Customers",
                column: "PartnerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Document",
                table: "Documents",
                column: "DocumentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EditDetail_DataModelKeyFieldValue",
                table: "EditDetails",
                columns: new[] { "DataModel", "KeyFieldValue" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCodeSUErrorCode",
                table: "ErrorCodeSUs",
                column: "ErrorCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationSU_IntegrationCode",
                table: "IntegrationSUs",
                columns: new[] { "IntegrationCode", "IntegrationType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch",
                table: "Location",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCode",
                table: "Location",
                column: "LocationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partner_PartnerCode",
                table: "Partners",
                column: "PartnerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partner_PartnerName",
                table: "Partners",
                column: "PartnerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipientConnection",
                table: "RecipientConnections",
                column: "RecipientConnectionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipientGroupCode",
                table: "RecipientGroups",
                column: "RecipientGroupCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipient",
                table: "Recipients",
                column: "RecipientCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysParameterSUParamCode",
                table: "SysParameterSUs",
                column: "ParamCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_UserGroup_GroupCode",
                table: "UserGroups",
                columns: new[] { "CompanyCode", "GroupCode", "CustomerCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CustomerCode",
                table: "Users",
                column: "CustomerCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountManagers");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "AppCtrlSUs");

            migrationBuilder.DropTable(
                name: "AppCtrlUserProfiles");

            migrationBuilder.DropTable(
                name: "ApplicationLog");

            migrationBuilder.DropTable(
                name: "AuditDetails");

            migrationBuilder.DropTable(
                name: "AuditMasters");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "DocSequenceSUs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "EditDetails");

            migrationBuilder.DropTable(
                name: "EmailMasters");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "ErrorCodeSUs");

            migrationBuilder.DropTable(
                name: "IntegrationSUs");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LoginSUs");

            migrationBuilder.DropTable(
                name: "LookupIndustrySU");

            migrationBuilder.DropTable(
                name: "MccCodeSUs");

            migrationBuilder.DropTable(
                name: "ModuleActionSUs");

            migrationBuilder.DropTable(
                name: "ModuleSU");

            migrationBuilder.DropTable(
                name: "Params");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "ReasonSU");

            migrationBuilder.DropTable(
                name: "RecipientConnectionDetails");

            migrationBuilder.DropTable(
                name: "RecipientConnections");

            migrationBuilder.DropTable(
                name: "RecipientGroups");

            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "RiskScoreSUs");

            migrationBuilder.DropTable(
                name: "SignUps");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "StatusSUs");

            migrationBuilder.DropTable(
                name: "SysParameterSUs");

            migrationBuilder.DropTable(
                name: "UserAccessLevels");

            migrationBuilder.DropTable(
                name: "UserGroupAccesses");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ParamSUs");

            migrationBuilder.AlterColumn<string>(
                name: "SettingsType",
                table: "SYS_Settings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SettingValue",
                table: "SYS_Settings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "SettingName",
                table: "SYS_Settings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }
    }
}
