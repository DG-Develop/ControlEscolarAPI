namespace Application.Responses
{
    public class PaginacionResponse<T> : ApiResponse<T>
    {
        public int NumeroPagina { get; set; }
        public int TamanioPagina { get; set; }

        public PaginacionResponse(T resultado, int numeroPagina, int tamanioPagina) {
            NumeroPagina = numeroPagina;
            TamanioPagina = tamanioPagina;
            Resultado = resultado;
            Mensaje = "";
            Exitoso = true;
            Errores = new List<string>();
        }
    }
}
