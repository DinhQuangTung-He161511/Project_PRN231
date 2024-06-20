using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedIndentityDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("ad0c8e8b-2c8f-4303-ba6a-10eb314c01cd"), "44f519a1-b012-472a-9a02-e1e076083de0", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ad0c8e8b-2c8f-4303-ba6a-10eb314c01cd"), new Guid("9fc83c68-62f8-4305-a688-ae1f92ce9200") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9fc83c68-62f8-4305-a688-ae1f92ce9200"), 0, "2e5d811a-d2ca-4a16-a0a7-a5869d780a63", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "tungdqhe161511@fpt.edu.vn", true, "Tung", "Quang", false, null, "tungdqhe161511@fpt.edu.vn", "admin", "AQAAAAEAACcQAAAAEIX7JNY6hE1e8KuZuJrG6Wu5dBpL0C2ra8hkzLpSrOCE+NRdOFt46m85CbBHrKCx4A==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 6, 20, 20, 55, 46, 149, DateTimeKind.Local).AddTicks(4749));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad0c8e8b-2c8f-4303-ba6a-10eb314c01cd"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ad0c8e8b-2c8f-4303-ba6a-10eb314c01cd"), new Guid("9fc83c68-62f8-4305-a688-ae1f92ce9200") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("9fc83c68-62f8-4305-a688-ae1f92ce9200"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 6, 20, 20, 53, 40, 4, DateTimeKind.Local).AddTicks(8629));
        }
    }
}
