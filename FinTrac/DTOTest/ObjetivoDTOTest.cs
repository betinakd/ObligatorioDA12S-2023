using DTO;
using DTO.EnumsDTO;
namespace DTOTest
{
	[TestClass]
	public class ObjetivoDTOTest
	{
		[TestMethod]
		public void ObjetivoDTO_Tiene_Titulo()
		{
			ObjetivoDTO objetivoDTO = new ObjetivoDTO();
			string titulo = "Objetivo 1";
			objetivoDTO.Titulo = titulo;
			Assert.AreEqual(titulo, objetivoDTO.Titulo);
		}

		[TestMethod]
		public void ObjetivoDTO_Tiene_MontoMaximo()
		{
			ObjetivoDTO objetivoDTO = new ObjetivoDTO();
			double montoMaximo = 1000;
			objetivoDTO.MontoMaximo = montoMaximo;
			Assert.AreEqual(montoMaximo, objetivoDTO.MontoMaximo);
		}

		[TestMethod]
		public void ObjetivoDTO_Tiene_Categorias()
		{
			ObjetivoDTO objetivoDTO = new ObjetivoDTO();
			List<CategoriaDTO> categorias = new List<CategoriaDTO>();
			var categoria = new CategoriaDTO()
			{
				Nombre = "Categoria 1",
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoriaDTO.Costo
			};
			categorias.Add(categoria);
			objetivoDTO.Categorias = categorias;
			Assert.AreEqual(categorias, objetivoDTO.Categorias);
			Assert.AreEqual(1, objetivoDTO.Categorias.Count);
		}
	}
}
