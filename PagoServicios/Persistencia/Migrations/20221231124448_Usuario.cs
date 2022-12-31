using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaPagar_Cliente_ClienteID",
                table: "CuentaPagar");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "CuentaPagar",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_CuentaPagar_ClienteID",
                table: "CuentaPagar",
                newName: "IX_CuentaPagar_UsuarioID");

            migrationBuilder.AddColumn<decimal>(
                name: "cuota",
                table: "CuentaPagar",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    NIS = table.Column<string>(type: "text", nullable: false),
                    numerocedula = table.Column<int>(name: "numero_cedula", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaPagar_Usuario_UsuarioID",
                table: "CuentaPagar",
                column: "UsuarioID",
                principalTable: "Usuario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CuentaPagar_Usuario_UsuarioID",
                table: "CuentaPagar");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropColumn(
                name: "cuota",
                table: "CuentaPagar");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "CuentaPagar",
                newName: "ClienteID");

            migrationBuilder.RenameIndex(
                name: "IX_CuentaPagar_UsuarioID",
                table: "CuentaPagar",
                newName: "IX_CuentaPagar_ClienteID");

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NIS = table.Column<string>(type: "text", nullable: false),
                    apellido = table.Column<string>(type: "text", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    numerocedula = table.Column<int>(name: "numero_cedula", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CuentaPagar_Cliente_ClienteID",
                table: "CuentaPagar",
                column: "ClienteID",
                principalTable: "Cliente",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
