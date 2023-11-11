using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;

namespace Controlador
{
	public class ControladorCategorias
	{
		private EspacioLogic _categoriaLogic;

		public ControladorCategorias(EspacioLogic categoriaLogic)
		{
			_categoriaLogic = categoriaLogic;
		}

		public List<CategoriaDTO> CategoriasDeEspacio(int id)
		{
			Espacio espacio = _categoriaLogic.FindEspacio(id);
			List<Categoria> categorias = espacio.Categorias;
			List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();
			foreach (Categoria categoria in categorias)
			{
				CategoriaDTO categoriaDTO = new CategoriaDTO()
				{
					Id = categoria.Id,
					Nombre = categoria.Nombre,
					EstadoActivo = categoria.EstadoActivo,
					FechaCreacion = categoria.FechaCreacion,
					Tipo = Cambiar_TipoCategoriaDTO(categoria.Tipo)
				};
				categoriasDTO.Add(categoriaDTO);
			}
			return categoriasDTO;
		}

		private TipoCategoriaDTO Cambiar_TipoCategoriaDTO(TipoCategoria tipoCategoria)
		{
			if (tipoCategoria.Equals(TipoCategoria.Costo))
			{
				return TipoCategoriaDTO.Costo;
			}
			return TipoCategoriaDTO.Ingreso;
		}
	}
}
