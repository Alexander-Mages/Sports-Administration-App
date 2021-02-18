using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class idek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "287edbaf-c553-4b32-a775-7b04d1b49640");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ebac15ce-b533-4971-a53c-998bb847a153");

            migrationBuilder.AddColumn<int>(
                name: "Identification",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Identification", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e4aa26c9-6597-4d58-88c4-2793a60f11e7", 0, "37723d57-82c0-424c-b9a4-ac0ed4db7ccb", "John@gmail.com", false, 1, false, null, "John", null, null, null, null, false, "03b523ba-6ab9-4d6b-8364-5bf5584d971a", "Swim", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Identification", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21233d93-6e43-4777-bb98-fcdbe82def2a", 0, "5629c208-bfee-4b8a-a482-abd0a23ec746", "Bill@gmail.com", false, 2, false, null, "Bill", null, null, null, null, false, "2491156b-9f22-4839-90de-ec3f7275d2ba", "Tennis", false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21233d93-6e43-4777-bb98-fcdbe82def2a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e4aa26c9-6597-4d58-88c4-2793a60f11e7");

            migrationBuilder.DropColumn(
                name: "Identification",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "287edbaf-c553-4b32-a775-7b04d1b49640", 0, "ec0fb1db-e5ad-4ca4-b5e3-5fd8a3638497", "John@gmail.com", false, false, null, "John", null, null, null, null, false, "762039a9-4bca-4f43-a7a9-2f5f8d2b9269", "Swim", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ebac15ce-b533-4971-a53c-998bb847a153", 0, "47f8bcb8-52c5-4223-98bf-f0ed48703825", "Bill@gmail.com", false, false, null, "Bill", null, null, null, null, false, "8462df35-3ac2-44f5-a475-c93db5e17d61", "Tennis", false, null });
        }
    }
}
