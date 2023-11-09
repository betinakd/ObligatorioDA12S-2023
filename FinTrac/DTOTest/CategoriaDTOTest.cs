using DTO;
using DTO.EnumsDTO;
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

		[TestMethod]
		public void CategoriaDTO_Tiene_Tipo()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			TipoCategoriaDTO tipo = TipoCategoriaDTO.Costo;
			categoria.Tipo = tipo;
			CategoriaDTO categoria2 = new CategoriaDTO();
			TipoCategoriaDTO tipo2 = TipoCategoriaDTO.Ingreso;
			categoria2.Tipo = tipo2;
			Assert.AreEqual(tipo, categoria.Tipo);
			Assert.AreEqual(tipo2, categoria2.Tipo);
		}

		[TestMethod]
		public void CategoriaDTO_Tiene_Id()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			categoria.Id = 1;
			Assert.AreEqual(1, categoria.Id);
		}
	}
}
