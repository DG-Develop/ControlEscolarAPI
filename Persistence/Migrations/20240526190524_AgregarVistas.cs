using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregarVistas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW VwPersonal AS
                SELECT 
                    TRIM(TRIM(Me.nombre) + ' ' + TRIM(Me.apellidos)) AS NombreCompleto,
                    Me.correo_electronico AS Correo,
                    Me.numero_control AS NumeroControl,
                    TP.descripcion AS TipoPersonal,
                    P.sueldo AS Sueldo
                FROM Personal AS P
                INNER JOIN MiembroEscolar AS ME
                ON Me.id_miembro_escolar = P.id_miembro_escolar
                INNER JOIN TipoPersonal AS TP
                ON TP.id_tipo_personal = Me.id_tipo_personal;
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW VwAlumno AS
                SELECT 
                    TRIM(TRIM(Me.nombre) + ' ' + TRIM(Me.apellidos)) AS NombreCompleto,
                    Me.correo_electronico AS Correo,
                    Me.numero_control AS NumeroControl,
                    TP.descripcion AS TipoPersonal,
                    A.grado AS GradoEscolar
                FROM Alumno AS A
                INNER JOIN MiembroEscolar AS ME
                ON Me.id_miembro_escolar = A.id_miembro_escolar
                INNER JOIN TipoPersonal AS TP
                ON TP.id_tipo_personal = Me.id_tipo_personal
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwPersonal;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwAlumno;");
        }
    }
}
