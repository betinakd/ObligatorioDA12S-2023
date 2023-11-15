using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class CategoriaGastoDTOTest
	{
		[TestMethod]
		public void CategoriaGastoDTO_Tiene_Categoria()
		{
			CategoriaDTO categoria = new CategoriaDTO()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Id = 1,
				Nombre = "PruebaCat",
				Tipo = TipoCategoriaDTO.Costo,
			};
			CategoriaGastoDTO categoriaGasto = new CategoriaGastoDTO();
			categoriaGasto.Categoria = categoria;
			Assert.IsFalse(categoriaGasto.Categoria.Equals(categoria));
		}
	}
}
