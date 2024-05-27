namespace Application.DTOs
{
    public class UpdatePersonalDTO
    {
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }
        public int IdTipoPersonal { get; set; }
        public decimal Sueldo { get; set; }
        public int IdMiembroEscolar { get; set; }
    }
}
