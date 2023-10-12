using Models.Domain;
using Models.Excepcion;

namespace ModelsTest
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
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Categoria_Tiene_Nombre_Nulo()
		{
			categoria.Nombre = null;
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Categoria_Tiene_Nombre_Vacio()
		{
			categoria.Nombre = "";
		}

		[TestMethod]
		public void Categoria_Tiene_FechaCreacion()
		{
			Assert.IsNotNull(categoria.FechaCreacion);
		}

		[TestMethod]
		public void Categoria_Tiene_Estado_Activo()
		{
			categoria.EstadoActivo=true;
			Assert.IsTrue(categoria.EstadoActivo);
		}

		[TestMethod]
		public void Categoria_Tiene_Tipo_Costo()
		{
			categoria.Tipo = TipoCategoria.Costo;
			Assert.AreEqual(TipoCategoria.Costo, categoria.Tipo);
		}

		[TestMethod]
		public void Set_Fecha_Creacion()
		{
			DateTime fecha = new DateTime(2015, 1, 1);
			categoria.FechaCreacion = fecha;
			Assert.AreEqual(fecha, categoria.FechaCreacion);
		}

		[TestMethod]
		public void Categoria_Equals_Null()
		{

			var categoria1 = new Categoria
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
			};
			Object objeto = null;
			var objeto2 = new Object();
			Assert.IsFalse(categoria1.Equals(objeto));
			Assert.IsFalse(categoria1.Equals(objeto2));
			Assert.IsFalse(categoria1.Equals(objeto2));
		}

		[TestMethod]
		public void Categoria_Equals_Diferentes()
		{
			var categoria1 = new Categoria
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
			};

			var categoria2 = new Categoria
			{
				Nombre = "CategoriaDiferentePrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
			};
			Assert.IsFalse(categoria1.Equals(categoria2));
		}

		[TestMethod]
		public void Categoria_Equals_Iguales()
		{
			var categoria1 = new Categoria
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
			};

			var categoria2 = new Categoria
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
			};
			Assert.IsTrue(categoria1.Equals(categoria2));
		}
	}
}
