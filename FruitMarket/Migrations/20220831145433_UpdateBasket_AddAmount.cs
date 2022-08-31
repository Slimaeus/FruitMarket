using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitMarket.Migrations
{
    public partial class UpdateBasket_AddAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Baskets");
        }
    }
}
