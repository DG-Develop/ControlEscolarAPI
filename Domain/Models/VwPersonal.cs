namespace Domain.Models
{
    public class VwPersonal
    {
        public string NombreCompleto { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NumeroControl { get; set; } = null!;
        public string TipoPersonal { get; set; } = null!;
        public decimal Sueldo { get; set; }
    }
}
