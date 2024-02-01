using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Relational.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SonGuncelleme4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdditionalProducts_ProductId",
                table: "AdditionalProducts");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalProducts_ProductId",
                table: "AdditionalProducts",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdditionalProducts_ProductId",
                table: "AdditionalProducts");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalProducts_ProductId",
                table: "AdditionalProducts",
                column: "ProductId");
        }
    }
}
