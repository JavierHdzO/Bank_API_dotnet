using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank_api.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Users_UserId",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Clients",
                type: "boolean",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_UserId",
                table: "Clients");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clients_UserId",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Users_UserId",
                table: "Client",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
