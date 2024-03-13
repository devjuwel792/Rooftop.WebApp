using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rooftop.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_farm_CartItemsId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_CartItemsId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CartItemsId",
                table: "Payment");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "farm",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FarmVm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    SquareFeet = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SomeDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoreDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseOwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmVm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmVm_HouseOwners_HouseOwnersId",
                        column: x => x.HouseOwnersId,
                        principalTable: "HouseOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_farm_PaymentId",
                table: "farm",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmVm_HouseOwnersId",
                table: "FarmVm",
                column: "HouseOwnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_farm_Payment_PaymentId",
                table: "farm",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_farm_Payment_PaymentId",
                table: "farm");

            migrationBuilder.DropTable(
                name: "FarmVm");

            migrationBuilder.DropIndex(
                name: "IX_farm_PaymentId",
                table: "farm");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "farm");

            migrationBuilder.AddColumn<int>(
                name: "CartItemsId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CartItemsId",
                table: "Payment",
                column: "CartItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_farm_CartItemsId",
                table: "Payment",
                column: "CartItemsId",
                principalTable: "farm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
