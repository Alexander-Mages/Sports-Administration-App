using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class athletedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AthleteDataId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AthleteData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FromAthleteDataId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonalRecord = table.Column<int>(type: "INTEGER", nullable: false),
                    AthleteDataId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Times_AthleteData_AthleteDataId",
                        column: x => x.AthleteDataId,
                        principalTable: "AthleteData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "deeba6cc-a62c-4133-8e8c-3b135be541e0", "93063428-5068-48d2-8936-f09701c1bc09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e5e91685-5795-445e-ad27-e471f7f8f438", "979978cb-bfd5-4caa-beb3-e0e5c71a5e15" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AthleteDataId",
                table: "AspNetUsers",
                column: "AthleteDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_AthleteDataId",
                table: "Times",
                column: "AthleteDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AthleteData_AthleteDataId",
                table: "AspNetUsers",
                column: "AthleteDataId",
                principalTable: "AthleteData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AthleteData_AthleteDataId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "AthleteData");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AthleteDataId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AthleteDataId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b3ed3e44-0c39-4062-9ca6-05dc1e02334f", "1793ae2f-4f0a-456a-9eeb-bd758d0315a0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e37809a8-a028-4206-85cf-fea5aac5814b", "6e260a9a-2c82-40ed-a6c0-26bacee45e99" });
        }
    }
}
