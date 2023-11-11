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
			try
			{
				Categoria nuevaCategoria = new Categoria()
				{
					Nombre = categoriaDTO.Nombre,
					Tipo = tipo,
					EstadoActivo = categoriaDTO.EstadoActivo,
					FechaCreacion = categoriaDTO.FechaCreacion
				};
				espacio.AgregarCategoria(nuevaCategoria);
				_categoriaLogic.UpdateEspacio(espacio);
			}
			catch (DomainEspacioException e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarNombreCategoria(int id, CategoriaDTO categoriaDTO, string nuevoNombre)
		{
			string msjError = "";
			Espacio espacio = _categoriaLogic.FindEspacio(id);
			Categoria categoria = Cambiar_A_Categoria(id, categoriaDTO.Id);
			List<Categoria> categorias = espacio.Categorias;
			if (!categorias.Any(c => c.Nombre == nuevoNombre))
			{
				categoria.Nombre = nuevoNombre;
				_categoriaLogic.UpdateEspacio(espacio);
			}
			else
			{
				msjError = "Ya hay categorias con ese nombre.";
			}
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

		private Categoria Cambiar_A_Categoria(int id, int idCategoriaDTO)
		{
			Categoria categoriaResultado = null;
			foreach (Categoria categoria in _categoriaLogic.FindEspacio(id).Categorias)
			{
				if (categoria.Id == idCategoriaDTO)
				{
					categoriaResultado = categoria;
				}
			}
			return categoriaResultado;
		}
	}
}
