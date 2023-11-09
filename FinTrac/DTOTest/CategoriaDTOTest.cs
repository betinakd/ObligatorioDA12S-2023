using DTO;

namespace DTOTest
{
	[TestClass]
	public class CategoriaDTOTest
	{
		[TestMethod]
		public void CategoriaDTO_Tiene_Nombre()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			categoria.Nombre = "Categoria";
			Assert.AreEqual("Categoria", categoria.Nombre);
		}

		[TestMethod]
		public void CategoriaDTO_Tiene_EstadoActivo()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			categoria.EstadoActivo = true;
			Assert.AreEqual(true, categoria.EstadoActivo);
		}

		[TestMethod]
		public void CategoriaDTO_Tiene_FechaCreacion()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			DateTime fecha = DateTime.Now;
			categoria.FechaCreacion = fecha;
			Assert.AreEqual(fecha, categoria.FechaCreacion);
		}
	}
}
