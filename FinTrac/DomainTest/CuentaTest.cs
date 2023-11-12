using Domain;
using Excepcion;

namespace DomainTest
{
	[TestClass]
	public class CuentaTest
	{
		private Cuenta cuenta;

		[TestInitialize]
		public void Inicializar()
		{
			cuenta = new Cuenta();
		}

		[TestMethod]
		public void Nueva_Cuenta_No_Nula()
		{
			Assert.IsNotNull(cuenta);
		}

		[TestMethod]
		public void Cuenta_Tiene_Moneda_Pesos_Uruguayos()
		{
			cuenta.Moneda = TipoCambiario.PesosUruguayos;
			Assert.AreEqual(cuenta.Moneda, TipoCambiario.PesosUruguayos);
		}

		[TestMethod]
		public void Cuenta_Tiene_Moneda_Dolar()
		{
			cuenta.Moneda = TipoCambiario.Dolar;
			Assert.AreEqual(cuenta.Moneda, TipoCambiario.Dolar);
		}

		[TestMethod]
		public void Cuenta_Tiene_FechaCreacion()
		{
			Assert.IsNotNull(cuenta.FechaCreacion);
		}


		[TestMethod]
		public void Cuenta_Tiene_ToString()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.PesosUruguayos;
			string fecha = DateTime.Now.ToString();
			Assert.AreEqual(cuenta.ToString(), "Pesos Uruguayos - ");
		}

		[TestMethod]
		public void Cuenta_Tiene_ToString_Dolar()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.Dolar;
			string fecha = DateTime.Now.ToString();
			Assert.AreEqual(cuenta.ToString(), "Dolar - ");
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Modificar_Cuenta_Excepcion_No_Implementada()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Modificar(cuenta);
		}

		[TestMethod]
		public void Cuenta_Tiene_Moneda_Euro()
		{
			cuenta.Moneda = TipoCambiario.Euro;
			Assert.AreEqual(cuenta.Moneda, TipoCambiario.Euro);
		}

		[TestMethod]
		public void Cuenta_Tiene_ToString_Euro()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Moneda = TipoCambiario.Euro;
			string fecha = DateTime.Now.ToString();
			Assert.AreEqual(cuenta.ToString(), "Euro - ");
		}

		[TestMethod]
		public void Cuenta_Tiene_Id()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.Id = 1;
			Assert.AreEqual(cuenta.Id, 1);
		}

		[TestMethod]
		public void Cuenta_Tiene_EspacioId()
		{
			Cuenta cuenta = new Cuenta();
			cuenta.EspacioId = 1;
			Assert.AreEqual(cuenta.EspacioId, 1);
		}

		[TestMethod]
		public void Cuenta_Tiene_Espacio()
		{
			Cuenta cuenta = new Cuenta();
			Espacio espacio = new Espacio();
			cuenta.Espacio = espacio;
			Assert.AreEqual(cuenta.Espacio, espacio);
		}

		[TestMethod]
		public void Cuenta_Tiene_Transacciones()
		{
			Categoria categoriaTest = new Categoria()
			{
				Nombre = "CategoriaPrueba",
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				FechaCreacion = new DateTime(2015, 1, 1),
			};
			Ahorro cuentaTest = new Ahorro()
			{
				Nombre = "CuentaPrueba",
				Moneda = TipoCambiario.Dolar,
				Saldo = 100,
			};
			TransaccionCosto transaccionTest = new TransaccionCosto()
			{
				Titulo = "TransaccionPrueba",
				Monto = 100,
				Moneda = TipoCambiario.Dolar,
				CategoriaTransaccion = categoriaTest,
				CuentaMonetaria = cuentaTest
			};
			List<Transaccion> transacciones = new List<Transaccion>();
			transacciones.Add(transaccionTest);
			cuentaTest.Transacciones = transacciones;
			Assert.AreEqual(1, cuentaTest.Transacciones.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Cuenta_Tiene_Metodo_TipoCuenta_NoImplementado()
		{
			Cuenta cuentaTest = new Cuenta()
			{
				FechaCreacion = new DateTime(2015, 1, 1),
				Moneda = TipoCambiario.Dolar,
			};
			cuentaTest.TipoDeCuenta();
		}
	}
}
