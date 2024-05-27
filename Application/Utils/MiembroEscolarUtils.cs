using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Application.Utils
{
    public static class MiembroEscolarUtils
    {
        public static async Task<string?> GenerarNumeroControl(string IdentificadorDeControl, string ConexionString)
        {
            string numeroControl;

            using(var conexion = new SqlConnection(ConexionString))
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
