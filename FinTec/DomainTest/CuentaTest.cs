using Domain;
using System.Data.SqlTypes;

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
		public void Recibir_Deposito()
		{
			cuenta.RecibirDeposito(100);
		}
	}
}
