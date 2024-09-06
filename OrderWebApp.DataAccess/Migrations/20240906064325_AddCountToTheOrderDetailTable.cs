using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderWebApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCountToTheOrderDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "count",
                table: "OrderDetails");
        }
    }
}
