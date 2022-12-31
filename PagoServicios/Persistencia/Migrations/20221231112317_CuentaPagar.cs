using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class CuentaPagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_servicio",
                table: "servicio");

            migrationBuilder.RenameTable(
                name: "servicio",
                newName: "Servicio");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Servicio",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicio",
                table: "Servicio",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Cliente",
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
                    table.PrimaryKey("PK_Cliente", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CuentaPagar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Concepto = table.Column<string>(type: "text", nullable: false),
                    ClienteID = table.Column<int>(type: "integer", nullable: false),
                    Importe = table.Column<decimal>(type: "numeric", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false),
                    ServicioID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaPagar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CuentaPagar_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuentaPagar_Servicio_ServicioID",
                        column: x => x.ServicioID,
                        principalTable: "Servicio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPagar_ClienteID",
                table: "CuentaPagar",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaPagar_ServicioID",
                table: "CuentaPagar",
                column: "ServicioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuentaPagar");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicio",
                table: "Servicio");

            migrationBuilder.RenameTable(
                name: "Servicio",
                newName: "servicio");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "servicio",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_servicio",
                table: "servicio",
                column: "ID");
        }
    }
}
