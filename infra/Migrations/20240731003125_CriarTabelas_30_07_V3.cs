using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanchonete.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelas_30_07_V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeProduto",
                table: "PedidoItem",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeProduto",
                table: "PedidoItem");
        }
    }
}
