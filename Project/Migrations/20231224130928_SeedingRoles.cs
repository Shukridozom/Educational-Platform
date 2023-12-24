using Microsoft.EntityFrameworkCore.Migrations;
using Project.Models;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Roles(Name) VALUES('{RoleName.Admin}')");
            migrationBuilder.Sql($"INSERT INTO Roles(Name) VALUES('{RoleName.Author}')");
            migrationBuilder.Sql($"INSERT INTO Roles(Name) VALUES('{RoleName.Student}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM Roles");
        }
    }
}
