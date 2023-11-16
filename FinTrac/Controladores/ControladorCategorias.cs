using LogicaNegocio;
using DTO;
using DTO.EnumsDTO;
using Dominio;
using Excepcion;

namespace Controlador
{
	public class ControladorCategorias
	{
		private EspacioLogic _espacioLogic;

		public ControladorCategorias(EspacioLogica categoriaLogic)
		{
			_espacioLogic = categoriaLogic;
		}

		public List<CategoriaDTO> CategoriasDeEspacio(int id)
		{
			Espacio espacio = _espacioLogic.FindEspacio(id);
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
			Espacio espacio = _espacioLogic.FindEspacio(id);
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
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarNombreCategoria(int id, CategoriaDTO categoriaDTO)
		{
			string msjError = "";
			Espacio espacio = _espacioLogic.FindEspacio(id);
			Categoria categoria = Cambiar_A_Categoria(id, categoriaDTO.Id);
			List<Categoria> categorias = espacio.Categorias;
			try
			{
				if (!categorias.Any(c => c.Nombre == categoriaDTO.Nombre))
				{
					categoria.Nombre = categoriaDTO.Nombre;
					_espacioLogic.UpdateEspacio(espacio);
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
			Espacio espacio = _espacioLogic.FindEspacio(Id);
			Categoria categoria = Cambiar_A_Categoria(Id, categoriaDTO.Id);
			try
			{
				espacio.BorrarCategoria(categoria);
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				errorMsj = e.Message;
			}
			return errorMsj;
		}

		public void ModificarEstadoCategoria(int Id, CategoriaDTO categoriaDTO)
		{
			Espacio espacio = _espacioLogic.FindEspacio(Id);
			Categoria categoria = Cambiar_A_Categoria(Id, categoriaDTO.Id);
			categoria.EstadoActivo = categoriaDTO.EstadoActivo;
			_espacioLogic.UpdateEspacio(espacio);
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
			Espacio espacio = _espacioLogic.FindEspacio(id);
			List<Categoria> categorias = espacio.Categorias;
			Categoria categoria = categorias.Find(c => c.Id == idCategoriaDTO);
			return categoria;
		}
	}
}
