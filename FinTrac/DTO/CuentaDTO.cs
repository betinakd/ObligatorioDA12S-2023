using DTO.EnumsDTO;
namespace DTO
{
	public class CuentaDTO
	{
		public int Id { get; set; }
		public TipoCambiarioDTO Moneda { get; set; }
		public DateTime FechaCreacion { get; set; }
		public double Saldo { get; set; }
	}
}
