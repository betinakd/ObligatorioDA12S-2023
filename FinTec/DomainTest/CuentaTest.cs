using Domain;

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

		
	}
}
