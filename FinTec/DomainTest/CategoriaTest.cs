using Domain;

namespace DomainTest
{
	[TestClass]
	public class CategoriaTest
	{
		private Categoria categoria;

		[TestInitialize]
		public void Inicializar()
		{
			categoria = new Categoria();
		}

		[TestMethod]
		public void Nueva_Categoria()
		{
			Assert.IsNotNull(categoria);
		}

		[TestMethod]
		public void Categoria_Tiene_Nombre()
		{
			string nombre = "CategoriaPrueba";
			categoria.Nombre = nombre;
			Assert.AreEqual(nombre, categoria.Nombre);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCategoriaException))]
		public void Excepcion_Categoria_Tiene_Nombre_Nulo()
		{
			categoria.Nombre = null;
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCategoriaException))]
		public void Excepcion_Categoria_Tiene_Nombre_Vacio()
		{
			categoria.Nombre = "";
		}

		[TestMethod]
		public void Categoria_Tiene_FechaCreacion()
		{
			Assert.IsNotNull(categoria.FechaCreacion);
		}
	}
}
