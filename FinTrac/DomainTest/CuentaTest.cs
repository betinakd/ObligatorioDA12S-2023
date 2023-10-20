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
		[ExpectedException(typeof(NotImplementedException))]
		public void Excepcion_Recibir_Deposito()
		{
			cuenta.IngresoMonetario(100);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Excepcion_Realizar_Deposito()
		{
			cuenta.EgresoMonetario(100);
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
	}
}
