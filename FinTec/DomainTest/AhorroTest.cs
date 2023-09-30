using Domain;
namespace DomainTest
{
	[TestClass]
	public class AhorroTest
	{
		private Cuenta cuentaAhorro;

		[TestInitialize]
		public void Inicializar()
		{
			Cuenta cuentaAhorro = new Ahorro();
		}

		[TestMethod]
		public void Nueva_Cuenta_Ahorro_No_Nula()
		{
			Assert.IsNotNull(cuentaAhorro);
		}
	}
}
