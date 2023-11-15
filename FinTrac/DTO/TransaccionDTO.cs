using DTO.EnumsDTO;

namespace DTO
{
	public class TransaccionDTO
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public DateTime FechaTransaccion { get; set; }
		public double Monto { get; set; }
		public TipoCambiarioDTO Moneda { get; set; }
		public string CuentaMonetaria { get; set; }
		public string CategoriaTransaccion { get; set; }
		public string Tipo { get; set; }
	}
}
