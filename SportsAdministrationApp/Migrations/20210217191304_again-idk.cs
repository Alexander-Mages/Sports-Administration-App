using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsAdministrationApp.Migrations
{
    public partial class againidk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Team",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c97d813b-b341-4785-b490-1c9b288753fc", 0, "daaef196-d488-4f69-b047-fe000890c653", "User", "John@gmail.com", false, false, null, "John", null, null, null, null, false, "a87031ee-7b92-4228-844a-99da739c8520", "Swim", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Team", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a0a94521-b512-4a4b-ac73-c73b7c247af9", 0, "6898bca3-87d5-412a-a8ab-1d1a8db3798e", "User", "Bill@gmail.com", false, false, null, "Bill", null, null, null, null, false, "78746fb6-b270-48b6-881b-39f393664f01", "Tennis", false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0a94521-b512-4a4b-ac73-c73b7c247af9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c97d813b-b341-4785-b490-1c9b288753fc");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Team",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

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
    }
}
