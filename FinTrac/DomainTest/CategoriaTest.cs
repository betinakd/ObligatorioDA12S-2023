using Domain;
using Excepcion;

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
			categoria.EstadoActivo = true;
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

		[TestMethod]
		public void Categoria_Tiene_Id()
		{
			Categoria categoriaTest = new Categoria();
			categoriaTest.Id = 1;
			Assert.AreEqual(1, categoriaTest.Id);
		}

		[TestMethod]
		public void Categoria_Tiene_Espacio()
		{
			Categoria categoriaTest = new Categoria();
			Espacio espacio = new Espacio();
			categoriaTest.Espacio = espacio;
			Assert.AreEqual(espacio, categoriaTest.Espacio);
		}

		[TestMethod]
		public void Categoria_Tiene_EspacioId()
		{
			Categoria categoriaTest = new Categoria();
			categoriaTest.EspacioId = 1;
			Assert.AreEqual(1, categoriaTest.EspacioId);
		}

		[TestMethod]
		public void Categoria_Tiene_Objetivos()
		{
			Categoria categoriaTest = new Categoria()
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				FechaCreacion = new DateTime(2015, 1, 1),
			};
			List<Categoria> categorias = new List<Categoria>();
			categorias.Add(categoriaTest);
			Objetivo objetivoTest = new Objetivo()
			{
				Titulo = "ObjetivoPrueba",
				MontoMaximo = 500,
				Categorias = categorias,
			};
			List<Objetivo> objetivos = new List<Objetivo>();
			objetivos.Add(objetivoTest);
			categoriaTest.Objetivos = objetivos;
			Assert.AreEqual(1, categoriaTest.Objetivos.Count);
		}

		[TestMethod]
		public void Categoria_Tiene_Transacciones()
		{
			Categoria categoriaTest = new Categoria()
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				FechaCreacion = new DateTime(2015, 1, 1),
			};
			TransaccionCosto transaccionTest = new TransaccionCosto()
			{
				Titulo = "TransaccionPrueba",
				Monto = 100,
				Moneda = TipoCambiario.Dolar,
				CategoriaTransaccion = categoriaTest,
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "CuentaPrueba",
					Moneda = TipoCambiario.Dolar,
					Monto = 100,
				},
			};
			categoriaTest.Transacciones = new List<Transaccion> { transaccionTest };
			Assert.AreEqual(1, categoriaTest.Transacciones.Count);
		}
	}
}

