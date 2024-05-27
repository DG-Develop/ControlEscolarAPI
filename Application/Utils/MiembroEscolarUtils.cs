using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Application.Utils
{
    /// <summary>
    /// Clase estática que proporciona utilidades para manejar operaciones relacionadas con los miembros escolares.
    /// </summary>
    public static class MiembroEscolarUtils
    {
        /// <summary>
        /// Genera un número de control para un miembro escolar utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="IdentificadorDeControl">Prefijo que identifica el tipo de miembro escolar.</param>
        /// <param name="ConexionString">Cadena de conexión a la base de datos.</param>
        /// <returns>El número de control generado, o null si no se puede generar.</returns>
        public static async Task<string?> GenerarNumeroControl(string IdentificadorDeControl, string ConexionString)
        {
            string numeroControl;

            using (var conexion = new SqlConnection(ConexionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Prefijo", IdentificadorDeControl ?? string.Empty);
                parameters.Add("@NumeroControl", dbType: DbType.String, size: 20, direction: ParameterDirection.Output);

                await conexion.ExecuteAsync("SPGenerarNumeroControl", parameters, commandType: CommandType.StoredProcedure);

                numeroControl = parameters.Get<string>("@NumeroControl");
            }

            return numeroControl;
        }
    }
}
