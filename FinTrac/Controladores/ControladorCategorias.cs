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

		public string CrearCategoria(int id, CategoriaDTO categoriaDTO)
		{
			string msjError = "";
			Espacio espacio = _categoriaLogic.FindEspacio(id);
			TipoCategoria tipo = Cambiar_TipoCategoria(categoriaDTO.Tipo);
				Categoria nuevaCategoria = new Categoria()
				{
					Nombre = categoriaDTO.Nombre,
					Tipo = tipo,
					EstadoActivo = categoriaDTO.EstadoActivo,
					FechaCreacion = categoriaDTO.FechaCreacion
				};
				espacio.AgregarCategoria(nuevaCategoria);
				_categoriaLogic.UpdateEspacio(espacio);
			return msjError;
		}

		private TipoCategoriaDTO Cambiar_TipoCategoriaDTO(TipoCategoria tipoCategoria)
		{
			if (tipoCategoria.Equals(TipoCategoria.Costo))
			{
				return TipoCategoriaDTO.Costo;
			}
			return TipoCategoriaDTO.Ingreso;
		}

		private TipoCategoria Cambiar_TipoCategoria(TipoCategoriaDTO tipoCategoria)
		{
			if (tipoCategoria.Equals(TipoCategoriaDTO.Costo))
			{
				return TipoCategoria.Costo;
			}
			return TipoCategoria.Ingreso;
		}
	}
}
