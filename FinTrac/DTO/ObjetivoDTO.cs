namespace DTO
{
	public class ObjetivoDTO
	{ 
		public int Id { get; set; }
		public string Titulo { get; set; }
		public double MontoMaximo { get; set; }
		public List<CategoriaDTO> Categorias { get; set; }
		public string? Token { get; set; }
	}
}
