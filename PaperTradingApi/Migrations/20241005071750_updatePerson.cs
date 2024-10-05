using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperTradingApi.Migrations
{
    /// <inheritdoc />
    public partial class updatePerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAllOrders",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockTicker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAllOrders", x => new { x.UserName, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_UserAllOrders_UserDetail_UserName",
                        column: x => x.UserName,
                        principalTable: "UserDetail",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAllOrders");
        }
    }
}
