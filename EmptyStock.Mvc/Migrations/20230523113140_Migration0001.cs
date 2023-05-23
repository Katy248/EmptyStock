using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmptyStock.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class Migration0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductActions_RequestId",
                table: "ProductActions");

            migrationBuilder.CreateIndex(
                name: "IX_ProductActions_RequestId",
                table: "ProductActions",
                column: "RequestId",
                unique: true,
                filter: "[RequestId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductActions_RequestId",
                table: "ProductActions");

            migrationBuilder.CreateIndex(
                name: "IX_ProductActions_RequestId",
                table: "ProductActions",
                column: "RequestId");
        }
    }
}
