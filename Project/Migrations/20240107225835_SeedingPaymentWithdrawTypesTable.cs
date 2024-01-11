using Microsoft.EntityFrameworkCore.Migrations;
using Project.Core.Domains;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SeedingPaymentWithdrawTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO PaymentWithdrawTypes (Name) VALUES ('{TransferTypeNames.Payment}')");
            migrationBuilder.Sql($"INSERT INTO PaymentWithdrawTypes (Name) VALUES ('{TransferTypeNames.Withdraw}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM PaymentWithdrawTypes");
        }
    }
}
