using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedureForCreatingAdminUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
CREATE PROCEDURE CreateAdminUser(
IN username VARCHAR(50), 
IN email VARCHAR(255), 
IN passwordHash VARCHAR(255), 
IN firstName VARCHAR(255), 
IN lastName VARCHAR(255))
BEGIN
INSERT INTO users (username, email, passwordHash, firstName, lastName, roleId)
VALUES(username, email, passwordHash, firstName, lastName, (SELECT id FROM roles where name = 'Admin'));
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateAdminUser");
        }
    }
}
