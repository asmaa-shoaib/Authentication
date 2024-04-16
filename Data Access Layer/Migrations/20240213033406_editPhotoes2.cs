using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editPhotoes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Cars_CarID",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "Photos",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_CarID",
                table: "Photos",
                newName: "IX_Photos_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Photos",
                newName: "CarID");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_CarId",
                table: "Photos",
                newName: "IX_Photos_CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Cars_CarID",
                table: "Photos",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
