namespace Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CorreoElectronico { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string TipoUsuario { get; set; } = null!;
    }
}
