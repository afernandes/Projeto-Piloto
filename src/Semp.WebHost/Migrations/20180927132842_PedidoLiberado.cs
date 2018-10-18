using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Semp.WebHost.Migrations
{
    public partial class PedidoLiberado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Integrator_PedidoLiberado",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pedido = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Regra = table.Column<string>(nullable: true),
                    LiberadoPorId = table.Column<long>(nullable: true),
                    LiberadoEm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrator_PedidoLiberado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Integrator_PedidoLiberado_Core_User_LiberadoPorId",
                        column: x => x.LiberadoPorId,
                        principalTable: "Core_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Integrator_PedidoLiberado_LiberadoPorId",
                table: "Integrator_PedidoLiberado",
                column: "LiberadoPorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Integrator_PedidoLiberado");
        }
    }
}
