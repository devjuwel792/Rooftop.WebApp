using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rooftop.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class updateTableFarm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_farm_Payment_PaymentId",
                table: "farm");

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

            migrationBuilder.CreateTable(
                name: "AdminVm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminVm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentVm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrasnsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPaymentConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CartItemsId = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentVm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentVm_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentVm_farm_CartItemsId",
                        column: x => x.CartItemsId,
                        principalTable: "farm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CartItemsId",
                table: "Payment",
                column: "CartItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVm_CartItemsId",
                table: "PaymentVm",
                column: "CartItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentVm_UserId",
                table: "PaymentVm",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_farm_CartItemsId",
                table: "Payment",
                column: "CartItemsId",
                principalTable: "farm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_farm_CartItemsId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "AdminVm");

            migrationBuilder.DropTable(
                name: "PaymentVm");

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

            migrationBuilder.CreateIndex(
                name: "IX_farm_PaymentId",
                table: "farm",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_farm_Payment_PaymentId",
                table: "farm",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");
        }
    }
}
