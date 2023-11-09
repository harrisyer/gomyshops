using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoMyShops.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Params_ParamSUs_ParamCode",
                table: "Params");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SysParameterSUs",
                table: "SysParameterSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusSUs",
                table: "StatusSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskScoreSUs",
                table: "RiskScoreSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamSUs",
                table: "ParamSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleActionSUs",
                table: "ModuleActionSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MccCodeSUs",
                table: "MccCodeSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginSUs",
                table: "LoginSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationSUs",
                table: "IntegrationSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorCodeSUs",
                table: "ErrorCodeSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocSequenceSUs",
                table: "DocSequenceSUs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCtrlSUs",
                table: "AppCtrlSUs");

            migrationBuilder.RenameTable(
                name: "SysParameterSUs",
                newName: "SysParameterSU");

            migrationBuilder.RenameTable(
                name: "StatusSUs",
                newName: "StatusSU");

            migrationBuilder.RenameTable(
                name: "RiskScoreSUs",
                newName: "RiskScoreSU");

            migrationBuilder.RenameTable(
                name: "ParamSUs",
                newName: "ParamSU");

            migrationBuilder.RenameTable(
                name: "ModuleActionSUs",
                newName: "ModuleActionSU");

            migrationBuilder.RenameTable(
                name: "MccCodeSUs",
                newName: "MccCodeSU");

            migrationBuilder.RenameTable(
                name: "LoginSUs",
                newName: "LoginSU");

            migrationBuilder.RenameTable(
                name: "IntegrationSUs",
                newName: "IntegrationSU");

            migrationBuilder.RenameTable(
                name: "ErrorCodeSUs",
                newName: "ErrorCodeSU");

            migrationBuilder.RenameTable(
                name: "DocSequenceSUs",
                newName: "DocSequenceSU");

            migrationBuilder.RenameTable(
                name: "AppCtrlSUs",
                newName: "AppCtrlSU");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysParameterSU",
                table: "SysParameterSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusSU",
                table: "StatusSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskScoreSU",
                table: "RiskScoreSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamSU",
                table: "ParamSU",
                column: "ParamCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleActionSU",
                table: "ModuleActionSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MccCodeSU",
                table: "MccCodeSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginSU",
                table: "LoginSU",
                column: "SecurityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationSU",
                table: "IntegrationSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorCodeSU",
                table: "ErrorCodeSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocSequenceSU",
                table: "DocSequenceSU",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCtrlSU",
                table: "AppCtrlSU",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Params_ParamSU_ParamCode",
                table: "Params",
                column: "ParamCode",
                principalTable: "ParamSU",
                principalColumn: "ParamCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Params_ParamSU_ParamCode",
                table: "Params");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SysParameterSU",
                table: "SysParameterSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusSU",
                table: "StatusSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RiskScoreSU",
                table: "RiskScoreSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamSU",
                table: "ParamSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleActionSU",
                table: "ModuleActionSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MccCodeSU",
                table: "MccCodeSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginSU",
                table: "LoginSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegrationSU",
                table: "IntegrationSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ErrorCodeSU",
                table: "ErrorCodeSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocSequenceSU",
                table: "DocSequenceSU");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCtrlSU",
                table: "AppCtrlSU");

            migrationBuilder.RenameTable(
                name: "SysParameterSU",
                newName: "SysParameterSUs");

            migrationBuilder.RenameTable(
                name: "StatusSU",
                newName: "StatusSUs");

            migrationBuilder.RenameTable(
                name: "RiskScoreSU",
                newName: "RiskScoreSUs");

            migrationBuilder.RenameTable(
                name: "ParamSU",
                newName: "ParamSUs");

            migrationBuilder.RenameTable(
                name: "ModuleActionSU",
                newName: "ModuleActionSUs");

            migrationBuilder.RenameTable(
                name: "MccCodeSU",
                newName: "MccCodeSUs");

            migrationBuilder.RenameTable(
                name: "LoginSU",
                newName: "LoginSUs");

            migrationBuilder.RenameTable(
                name: "IntegrationSU",
                newName: "IntegrationSUs");

            migrationBuilder.RenameTable(
                name: "ErrorCodeSU",
                newName: "ErrorCodeSUs");

            migrationBuilder.RenameTable(
                name: "DocSequenceSU",
                newName: "DocSequenceSUs");

            migrationBuilder.RenameTable(
                name: "AppCtrlSU",
                newName: "AppCtrlSUs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SysParameterSUs",
                table: "SysParameterSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusSUs",
                table: "StatusSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RiskScoreSUs",
                table: "RiskScoreSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamSUs",
                table: "ParamSUs",
                column: "ParamCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleActionSUs",
                table: "ModuleActionSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MccCodeSUs",
                table: "MccCodeSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginSUs",
                table: "LoginSUs",
                column: "SecurityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegrationSUs",
                table: "IntegrationSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ErrorCodeSUs",
                table: "ErrorCodeSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocSequenceSUs",
                table: "DocSequenceSUs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCtrlSUs",
                table: "AppCtrlSUs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Params_ParamSUs_ParamCode",
                table: "Params",
                column: "ParamCode",
                principalTable: "ParamSUs",
                principalColumn: "ParamCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
