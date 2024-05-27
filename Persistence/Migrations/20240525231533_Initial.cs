using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoPersonal",
                columns: table => new
                {
                    id_tipo_personal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tiene_sueldo = table.Column<bool>(type: "bit", nullable: false),
                    sueldo_minimo = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0m),
                    sueldo_maximo = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0m),
                    identificador_de_control = table.Column<string>(type: "char(1)", maxLength: 1, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_personal", x => x.id_tipo_personal);
                });

            migrationBuilder.CreateTable(
                name: "MiembroEscolar",
                columns: table => new
                {
                    id_miembro_escolar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    correo_electronico = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    numero_control = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    estatus = table.Column<bool>(type: "bit", nullable: false),
                    id_tipo_personal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiembroEscolar", x => x.id_miembro_escolar);
                    table.ForeignKey(
                        name: "FK_MiembroEscolar_TipoPersonal_id_tipo_personal",
                        column: x => x.id_tipo_personal,
                        principalTable: "TipoPersonal",
                        principalColumn: "id_tipo_personal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    id_miembro_escolar = table.Column<int>(type: "int", nullable: false),
                    grado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.id_miembro_escolar);
                    table.ForeignKey(
                        name: "FK_Alumno_MiembroEscolar_id_miembro_escolar",
                        column: x => x.id_miembro_escolar,
                        principalTable: "MiembroEscolar",
                        principalColumn: "id_miembro_escolar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    id_miembro_escolar = table.Column<int>(type: "int", nullable: false),
                    sueldo = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.id_miembro_escolar);
                    table.ForeignKey(
                        name: "FK_Personal_MiembroEscolar_id_miembro_escolar",
                        column: x => x.id_miembro_escolar,
                        principalTable: "MiembroEscolar",
                        principalColumn: "id_miembro_escolar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_correo_electronico",
                table: "MiembroEscolar",
                column: "correo_electronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MiembroEscolar_id_tipo_personal",
                table: "MiembroEscolar",
                column: "id_tipo_personal");

            migrationBuilder.CreateIndex(
                name: "idx_descripcion",
                table: "TipoPersonal",
                column: "descripcion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "MiembroEscolar");

            migrationBuilder.DropTable(
                name: "TipoPersonal");
        }
    }
}
