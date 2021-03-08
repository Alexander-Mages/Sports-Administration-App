using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class athlete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalRecordId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PR = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AthleteData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    FromPersonalRecordId = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<decimal>(type: "TEXT", nullable: false),
                    PersonalRecordId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteData_PersonalRecord_PersonalRecordId",
                        column: x => x.PersonalRecordId,
                        principalTable: "PersonalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5ed08d13-77af-45c4-ae0f-ae12c15cd835", "b36c102c-2563-4532-a58f-13815a8b5371" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "607e4f27-1706-4076-a490-c40e1217ca44", "a3d1a4c5-552d-457c-9f8d-25707250349b" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalRecordId",
                table: "AspNetUsers",
                column: "PersonalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteData_PersonalRecordId",
                table: "AthleteData",
                column: "PersonalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalRecord_PersonalRecordId",
                table: "AspNetUsers",
                column: "PersonalRecordId",
                principalTable: "PersonalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalRecord_PersonalRecordId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AthleteData");

            migrationBuilder.DropTable(
                name: "PersonalRecord");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalRecordId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonalRecordId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d4c9e679-29c3-4a22-b0af-9b9cb0fc3dcb", "fd8d37aa-3208-418a-8586-a2186cdf50b2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "71be973a-35f8-4e04-9ad0-8797230d54aa", "6774f208-ab85-44b2-a94d-3f19dd50064b" });
        }
    }
}
