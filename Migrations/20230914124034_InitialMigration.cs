using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "DebitAmount");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditAmount",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditAmount",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "DebitAmount",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
