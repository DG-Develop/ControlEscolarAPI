namespace Domain.Entities
{
    public class MiembroEscolar
    {
        public int IdMiembroEscolar { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string NumeroControl { get; set; } = null!;
        public bool Estatus { get; set; }

        public int IdTipoPersonal { get; set; }
        public TipoPersonal TipoPersonal { get; set; } = null!;
    }
}
