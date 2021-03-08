using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class loginlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TotpConfigured",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TotpEnabled",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "randomKey",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "368cee29-3660-4499-a504-be42cd846286", "57196c80-1e6e-4e21-8206-4b059a5715f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2013b615-322f-4955-bf4a-bd93f0032023", "c3d1fe7a-3cd6-44ed-8eff-102b50b29b95" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotpConfigured",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotpEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "randomKey",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "65e1dfa7-a9af-4c40-a5d6-f08123d14741", "3aa9f274-27b4-4062-9c8c-94b009e8b2e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "41fb3505-c937-4df3-a1d7-9eecf400d4d4", "934405f6-7b3c-4543-b4e6-4880b76abe5b" });
        }
    }
}
