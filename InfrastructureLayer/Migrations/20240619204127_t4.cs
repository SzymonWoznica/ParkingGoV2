using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVO.InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class t4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "RecordId",
                keyValue: 1,
                column: "UserId",
                value: "44bac4b2-efab-4bb3-afb6-6748fc876b64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "RecordId",
                keyValue: 1,
                column: "UserId",
                value: "00000000-0000-0000-0000-000000000000");
        }
    }
}
