using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMyShops.Data.Migrations
{
    /// <inheritdoc />
    public partial class SYS_Setting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS_DataSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_DataSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SYS_Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SettingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SettingValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Settings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS_DataSettings");

            migrationBuilder.DropTable(
                name: "SYS_Settings");
        }
    }
}
