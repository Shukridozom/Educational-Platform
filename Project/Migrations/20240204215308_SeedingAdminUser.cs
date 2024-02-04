using Microsoft.EntityFrameworkCore.Migrations;
using Project.Core.Domains;
using System.Security.Cryptography.X509Certificates;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string passwordHash = "$2y$10$ukNBIayBEqv.IJ/60zIuPOGjwVJVw7goY9CFYBOzRC7XojOV3/Iwu"; //Password: admin_
            migrationBuilder.Sql($"CALL CreateAdminUser('admin', 'admin@example.com', '{passwordHash}', 'admin', 'admin');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM users WHERE username = '{RoleName.Admin}';");
        }
    }
}
