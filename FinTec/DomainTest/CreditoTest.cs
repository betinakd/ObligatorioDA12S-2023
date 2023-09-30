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
	}
}
