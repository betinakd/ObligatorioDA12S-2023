using LogicaNegocio;
using DTO;
using DTO.EnumsDTO;
using Dominio;
using Excepcion;

namespace Controlador
{
	public class ControladorCategorias
	{
		private EspacioLogica _categoriaLogic;

		public ControladorCategorias(EspacioLogica categoriaLogic)
		{
			_categoriaLogic = categoriaLogic;
		}

		public List<CategoriaDTO> CategoriasDeEspacio(int id)
		{
			Espacio espacio = _categoriaLogic.EncontrarEspacio(id);
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
			Espacio espacio = _categoriaLogic.EncontrarEspacio(id);
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
			catch (DominioEspacioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarNombreCategoria(int id, CategoriaDTO categoriaDTO, string nuevoNombre)
		{
			string msjError = "";
			Espacio espacio = _categoriaLogic.EncontrarEspacio(id);
			Categoria categoria = Cambiar_A_Categoria(id, categoriaDTO.Id);
			List<Categoria> categorias = espacio.Categorias;
			try
			{
				if (!categorias.Any(c => c.Nombre == nuevoNombre))
				{
					categoria.Nombre = nuevoNombre;
					_categoriaLogic.UpdateEspacio(espacio);
				}
				else
				{
					throw new DominioEspacioExcepcion("Ya existe una categoría con ese nombre");
				}
			}
			catch (DominioEspacioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string EliminarCategoria(int Id, CategoriaDTO categoriaDTO)
		{
			string errorMsj = "";
			Espacio espacio = _categoriaLogic.EncontrarEspacio(Id);
			Categoria categoria = Cambiar_A_Categoria(Id, categoriaDTO.Id);
			try
			{
				espacio.BorrarCategoria(categoria);
				_categoriaLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				errorMsj = e.Message;
			}
			return errorMsj;
		}

		public void ModificarEstadoCategoria(int Id, CategoriaDTO categoriaDTO, bool estadoActivo)
		{
			Espacio espacio = _categoriaLogic.EncontrarEspacio(Id);
			Categoria categoria = Cambiar_A_Categoria(Id, categoriaDTO.Id);
			categoria.EstadoActivo = estadoActivo;
			_categoriaLogic.UpdateEspacio(espacio);
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
			foreach (Categoria categoria in _categoriaLogic.EncontrarEspacio(id).Categorias)
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
