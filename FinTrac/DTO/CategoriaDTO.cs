using DTO.EnumsDTO;

namespace DTO
{
	public class CategoriaDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public bool EstadoActivo { get; set; }
		public DateTime FechaCreacion { get; set; }
		public TipoCategoriaDTO Tipo { get; set; }
	}
}
