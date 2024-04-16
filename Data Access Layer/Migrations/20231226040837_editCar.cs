using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class editCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandID",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Cars",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BrandID",
                table: "Cars",
                newName: "IX_Cars_BrandId");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Cars",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                newName: "IX_Cars_BrandID");

            migrationBuilder.AlterColumn<int>(
                name: "BrandID",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandID",
                table: "Cars",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "ID");
        }
    }
}
