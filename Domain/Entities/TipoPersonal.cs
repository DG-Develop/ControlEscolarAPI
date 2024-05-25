namespace Domain.Entities
{
    public class TipoPersonal
    {
        public int IdTipoPersonal { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool TieneSueldo { get; set; }
        public decimal SueldoMinimo { get; set; }
        public decimal SueldoMaximo { get; set; }
        public string IdentificadorDeControl { get; set; } = null!;

        public List<MiembroEscolar> MiembrosEscolares { get; set; } = new List<MiembroEscolar>(); 
    }
}
