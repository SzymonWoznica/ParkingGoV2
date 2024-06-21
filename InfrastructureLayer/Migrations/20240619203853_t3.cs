using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVO.InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class t3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "RecordId", "EmailAddress", "Password", "RoleUser", "UserId" },
                values: new object[] { 1, "a@a.a", "a", 0, "00000000-0000-0000-0000-000000000000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "RecordId",
                keyValue: 1);
        }
    }
}
