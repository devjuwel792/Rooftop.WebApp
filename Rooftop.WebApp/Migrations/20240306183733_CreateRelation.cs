using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rooftop.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HouseOwnersId",
                table: "farm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserVm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVm", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_farm_HouseOwnersId",
                table: "farm",
                column: "HouseOwnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_farm_HouseOwners_HouseOwnersId",
                table: "farm",
                column: "HouseOwnersId",
                principalTable: "HouseOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_farm_HouseOwners_HouseOwnersId",
                table: "farm");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "UserVm");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_farm_HouseOwnersId",
                table: "farm");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "HouseOwnersId",
                table: "farm");
        }
    }
}
