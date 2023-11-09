using DTO;

namespace DTOTest
{
	[TestClass]
	public class ObjetivoDTOTest
	{
		[TestMethod]
		public void ObjetivoDTO_Tiene_Titulo()
		{
			ObjetivoDTO objetivoDTO = new ObjetivoDTO();
			string titulo = "Objetivo 1";
			objetivoDTO.Titulo = titulo;
			Assert.AreEqual(titulo, objetivoDTO.Titulo);
		}

		[TestMethod]
		public void ObjetivoDTO_Tiene_MontoMaximo()
		{
			ObjetivoDTO objetivoDTO = new ObjetivoDTO();
			double montoMaximo = 1000;
			objetivoDTO.MontoMaximo = montoMaximo;
			Assert.AreEqual(montoMaximo, objetivoDTO.MontoMaximo);
		}
	}
}
