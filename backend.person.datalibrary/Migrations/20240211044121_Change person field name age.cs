using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.person.datalibrary.Migrations
{
    /// <inheritdoc />
    public partial class Changepersonfieldnameage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AGE",
                table: "Person",
                newName: "Age");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Person",
                newName: "AGE");
        }
    }
}
