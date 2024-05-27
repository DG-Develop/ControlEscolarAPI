namespace Application.DTOs
{
    public class TipoPersonalDTO
    {
        public int IdTipoPersonal { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal SueldoMinimo { get; set; }
        public decimal SueldoMaximo { get; set; }
        public string IdentificadorDeControl { get; set; } = null!;
    }
}
