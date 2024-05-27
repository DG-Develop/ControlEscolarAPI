namespace Application.Responses
{
    public class ApiResponse<T>
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = null!;

        public List<string> Errores { get; set; } = new List<string>();

        public T? Resultado { get; set; }
        public ApiResponse() { }

        public ApiResponse(T resultado, string mensaje = "")
        {
            Exitoso = true;
            Mensaje = mensaje;
            Resultado = resultado;
        }

        public ApiResponse(string mensaje)
        {
            Exitoso = false;
            Mensaje = mensaje;
        }
    }
}
