using Models.Domain;
using Models.Excepcion;

namespace DomainTest
{
	[TestClass]
	public class TransaccionesTest
	{
		private Transaccion transaccion1;
		[TestInitialize]
		public void SetUp()
		{
			transaccion1 = new Transaccion() { Titulo = "Transaccion1", Monto = 100, Moneda = TipoCambiario.Dolar };
		}

		[TestMethod]
		public void Nueva_Transaccion()
		{
			var transaccion = new Transaccion();
			Assert.IsNotNull(transaccion);
		}

		[TestMethod]
		public void Titulo_Transaccion()
		{
			var transaccion = new Transaccion();
			string titulo = "Transaccion1";
			transaccion.Titulo = titulo;
			Assert.AreEqual(titulo, transaccion.Titulo);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Titulo_Transaccion_Vacio()
		{
			var transaccion = new Transaccion();
			string titulo = "";
			transaccion.Titulo = titulo;
		}

		[TestMethod]
		public void Tiene_Fecha_Transaccion()
		{
			var transaccion = new Transaccion();
			Assert.IsNotNull(transaccion.FechaTransaccion);
		}

		[TestMethod]
		public void Monto_Transaccion()
		{
			var transaccion = new Transaccion();
			double monto = 100;
			transaccion.Monto = monto;
			Assert.AreEqual(monto, transaccion.Monto);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Monto_Mayor_Cero()
		{
			var transaccion = new Transaccion();
			double monto = 0;
			transaccion.Monto = monto;
		}

		[TestMethod]
		public void Moneda_Transaccion()
		{
			var transaccion = new Transaccion();
			transaccion.Moneda = TipoCambiario.Dolar;
			Assert.AreEqual(TipoCambiario.Dolar, transaccion.Moneda);
		}

		[TestMethod]
		public void Cuenta_Transaccion()
		{
			var transaccion = new Transaccion();
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.Dolar;
			transaccion.Moneda = TipoCambiario.Dolar;
			transaccion.CuentaMonetaria = cuenta;
			Assert.AreEqual(cuenta, transaccion.CuentaMonetaria);
		}

		[TestMethod]
		public void Categoria_Transaccion()
		{
			var transaccion = new Transaccion();
			Categoria categoria = new Categoria();
			categoria.Nombre = "Categoria1";
			categoria.Tipo = TipoCategoria.Costo;
			categoria.EstadoActivo = true;
			transaccion.CategoriaTransaccion = categoria;
			Assert.AreEqual(categoria, transaccion.CategoriaTransaccion);
		}

		[TestMethod]
		public void Contador_Id_Transaccion()
		{
			Transaccion transaccion = new Transaccion();
			Assert.AreEqual(0, transaccion.IdTransaccion);
		}

		[TestMethod]
		public void Asignar_Id_Transaccion()
		{
			Transaccion transaccion = new Transaccion();
			transaccion.AsignarIdTransaccion();
			Assert.AreEqual(2, Transaccion._contadorIdTransaccion);
		}

		[TestMethod]
		public void Transaccion_Tiene_Fecha()
		{
			Transaccion transaccion = new Transaccion();
			transaccion.FechaTransaccion = new DateTime(2020, 1, 1);
			Assert.AreEqual(new DateTime(2020, 1, 1), transaccion.FechaTransaccion);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Transaccion_Clon()
		{ 
			transaccion1.ClonTransaccion(transaccion1);
		}

		[TestMethod]
		public void EncontrarCambio_Otra_Fecha()
		{
			Espacio espacio = new Espacio();
			Cambio cambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(cambio);
			Cuenta cuenta = new Cuenta { Moneda = TipoCambiario.Dolar, };
			espacio.AgregarCuenta(cuenta);
			Categoria cuategoria = new Categoria 
			{ 
				EstadoActivo = true,
				Nombre = "nombre",
				Tipo = TipoCategoria.Costo,
			};
			espacio.AgregarCategoria(cuategoria);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = cuategoria,
				CuentaMonetaria = cuenta,
				Moneda = TipoCambiario.Dolar,
				Monto = 1,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today.AddDays(-1),
			};
			Cambio cambio1 = transaccion.EncontrarCambio(espacio);
			Assert.IsTrue(cambio1.FechaDeCambio != transaccion.FechaTransaccion);
		}
	}
}
