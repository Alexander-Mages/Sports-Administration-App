using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class coach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoachCode",
                table: "Teams",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Coach",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "91ac5138-1430-44e6-b3a1-f8e3e2d12b90", "e6b9c3e7-8d12-43d2-9a1d-e4a581c73333" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bb95a3a3-8662-4fd9-8f75-8ddee586fe41", "012521af-620d-45e8-b559-2d27fccff39c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachCode",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Coach",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "56c8e76f-6520-4c62-b14a-113e4a3e397b", "9cfda343-ce18-4851-8ba1-a6aafbb63240" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "90a93da0-2f2f-488d-9d24-ddfd9dd94a9a", "ee83bbb6-d356-4c03-a0e3-35de33209f32" });
        }
    }
}
