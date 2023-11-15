using DTO;

namespace DTOTest
{
	[TestClass]
	public class IngresoEgresoDTOTest
	{
		[TestMethod]
		public void IngresoEgresoDTO_Tiene_Fecha()
		{
			IngresoEgresoDTO ingresoEgreso = new IngresoEgresoDTO();
			ingresoEgreso.Fecha = DateTime.Today;
			Assert.AreEqual(DateTime.Today, ingresoEgreso.Fecha);
		}

		[TestMethod]
		public void IngresoEgresoDTO_Tiene_Ingresos()
		{
			IngresoEgresoDTO ingresoEgreso = new IngresoEgresoDTO();
			double ingresos = 1000;
			ingresoEgreso.Ingresos = ingresos;
			Assert.AreEqual(ingresos, ingresoEgreso.Ingresos);
		}

		[TestMethod]
		public void IngresoEgresoDTO_Tiene_Egresos()
		{
			IngresoEgresoDTO ingresoEgreso = new IngresoEgresoDTO();
			double egresos = 1000;
			ingresoEgreso.Egresos = egresos;
			Assert.AreEqual(egresos, ingresoEgreso.Egresos);
		}
	}
}
