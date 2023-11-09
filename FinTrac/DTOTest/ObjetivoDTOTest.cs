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
	}
}
