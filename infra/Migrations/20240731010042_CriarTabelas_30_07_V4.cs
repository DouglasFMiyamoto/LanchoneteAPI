using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infra.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelas_30_07_V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stauts",
                table: "Pedidos",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "PedidoItem",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "PedidoItem");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pedidos",
                newName: "Stauts");
        }
    }
}
