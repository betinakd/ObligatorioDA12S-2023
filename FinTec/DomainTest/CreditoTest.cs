using Domain;
namespace DomainTest
{
	[TestClass]
	public class CreditoTest
	{
		[TestMethod]
		public void Nueva_Cuenta_Credito_No_Nula()
		{
			Credito cuentaCredito = new Credito();
			Assert.IsNotNull(cuentaCredito);
		}

		[TestMethod]
		public void Credito_Tiene_Banco_Emisor()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = "BancoEmisorPrueba";
			Assert.AreEqual("BancoEmisorPrueba", cuentaCredito.BancoEmisor);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_Credito_Tiene_Banco_Emisor_Vacio()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = "";
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_Credito_Tiene_Banco_Emisor_Nulo()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.BancoEmisor = null;
		}

		[TestMethod]
		public void Credito_Tiene_Numero_Tarjeta()
		{
			Credito cuentaCredito = new Credito();
			cuentaCredito.NumeroTarjeta = "1234";
			Assert.AreEqual("1234", cuentaCredito.NumeroTarjeta);
		}

		[TestMethod]
		public void Excepcion_Numero_Tarjeta_Menor_Cuatro_Digitos()
		{
			Credito cuentaCredito = new Credito();

			Assert.ThrowsException<DomainCuentaException>(() =>
			{
				cuentaCredito.NumeroTarjeta = "123";
			});
		}
	}
}
