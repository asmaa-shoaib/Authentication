using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class EditDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vilocty",
                table: "Details",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Details",
                newName: "Panoramic");

            migrationBuilder.AddColumn<double>(
                name: "Acceleration",
                table: "Details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BatteryEnergy",
                table: "Details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CLTC_CruisingRange",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cameras",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurbWeight",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPower",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumSspeed",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelYear",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Torque",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acceleration",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "BatteryEnergy",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "CLTC_CruisingRange",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Cameras",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "CurbWeight",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "MaxPower",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "MaximumSspeed",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "ModelYear",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "Torque",
                table: "Details");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Details",
                newName: "vilocty");

            migrationBuilder.RenameColumn(
                name: "Panoramic",
                table: "Details",
                newName: "Name");
        }
    }
}
