using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class usuario2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PagoRealizado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Observacion = table.Column<string>(type: "text", nullable: false),
                    Importe = table.Column<decimal>(type: "numeric", nullable: false),
                    CuentaPagarID = table.Column<int>(type: "integer", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoRealizado", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PagoRealizado_CuentaPagar_CuentaPagarID",
                        column: x => x.CuentaPagarID,
                        principalTable: "CuentaPagar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagoRealizado_CuentaPagarID",
                table: "PagoRealizado",
                column: "CuentaPagarID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagoRealizado");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Usuario");
        }
    }
}
