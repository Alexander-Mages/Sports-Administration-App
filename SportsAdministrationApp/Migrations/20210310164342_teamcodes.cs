using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class teamcodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Team",
                table: "AspNetUsers",
                newName: "TeamCode");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HeadCoach = table.Column<string>(type: "TEXT", nullable: true),
                    TeamCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "HeadCoach", "Name", "TeamCode" },
                values: new object[] { 1, "Mr. Foo", "Swim", "Swim12345" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "HeadCoach", "Name", "TeamCode" },
                values: new object[] { 2, "Mr. Bar", "Tennis", "Tennis12345" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TeamCode",
                table: "AspNetUsers",
                newName: "Team");

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
    }
}
