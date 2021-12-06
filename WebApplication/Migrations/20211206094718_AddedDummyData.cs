using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class AddedDummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "EmailAddress", "Name" },
                values: new object[] { 1, "jsmarius@hotmail.com", "Johan" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "EmailAddress", "Name" },
                values: new object[] { 2, "jaw.smarius@avans.nl", "JohanWerk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
