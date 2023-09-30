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
	}
}
