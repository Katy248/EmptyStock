using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmptyStock.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class Migration0008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shipment_ProductId1",
                table: "ProductActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductActions_ProductId1",
                table: "ProductActions",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductActions_Shipment_ProductId1",
                table: "ProductActions",
                column: "Shipment_ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductActions_Products_ProductId1",
                table: "ProductActions",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductActions_Products_Shipment_ProductId1",
                table: "ProductActions",
                column: "Shipment_ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductActions_Products_ProductId1",
                table: "ProductActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductActions_Products_Shipment_ProductId1",
                table: "ProductActions");

            migrationBuilder.DropIndex(
                name: "IX_ProductActions_ProductId1",
                table: "ProductActions");

            migrationBuilder.DropIndex(
                name: "IX_ProductActions_Shipment_ProductId1",
                table: "ProductActions");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductActions");

            migrationBuilder.DropColumn(
                name: "Shipment_ProductId1",
                table: "ProductActions");
        }
    }
}
