using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Data.Migrations
{
    public partial class EmployeeDepartmentRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartementID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartementID",
                table: "Employees",
                column: "DepartementID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departements_DepartementID",
                table: "Employees",
                column: "DepartementID",
                principalTable: "Departements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departements_DepartementID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartementID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartementID",
                table: "Employees");
        }
    }
}
