using Microsoft.EntityFrameworkCore.Migrations;

namespace Semp.WebHost.Migrations
{
    public partial class FixLocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localization_Resource_Localization_Culture_CultureId",
                table: "Localization_Resource");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Localization_Culture");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Localization_Resource",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CultureId",
                table: "Localization_Resource",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Localization_Culture",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "Core_User",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Localization_Resource_Localization_Culture_CultureId",
                table: "Localization_Resource",
                column: "CultureId",
                principalTable: "Localization_Culture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localization_Resource_Localization_Culture_CultureId",
                table: "Localization_Resource");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "Core_User");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Localization_Resource",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "CultureId",
                table: "Localization_Resource",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Localization_Culture",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Localization_Culture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Localization_Culture",
                keyColumn: "Id",
                keyValue: "en-US",
                column: "IsDefault",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Localization_Resource_Localization_Culture_CultureId",
                table: "Localization_Resource",
                column: "CultureId",
                principalTable: "Localization_Culture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
