using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class coachcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4c5cfdea-afe0-4c73-b5d4-8d0e810c6cf5", "becacb09-e1ab-42e5-9119-d1330172128f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "72d53754-0d51-4ca3-bbaf-0a82b2df6c3b", "bf372e6f-55d3-4263-aa74-d5719c852a6c" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoachCode",
                value: "anothacode");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoachCode",
                value: "code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoachCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoachCode",
                value: null);
        }
    }
}
