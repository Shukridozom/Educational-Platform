using Microsoft.EntityFrameworkCore.Migrations;
using Project.Core.Domains;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedingSystemVariablesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO SystemVariables(Name, Value) VALUES('{SystemVariablesName.ProfitPercentage}', '0.2')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM SystemVariables");
        }
    }
}
