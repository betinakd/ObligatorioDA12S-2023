using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;

namespace Controlador
{
	public class ControladorObjetivos
	{
		private EspacioLogic _objetivoLogic;

		public ControladorObjetivos(EspacioLogic objetivoLogic)
		{
			_objetivoLogic = objetivoLogic;
		}

		public List<ObjetivoDTO> ObjetivosDeEspacio(int id)
		{
			Espacio espacio = _objetivoLogic.FindEspacio(id);
			List<Objetivo> objetivos = espacio.Objetivos;
			List<ObjetivoDTO> objetivosDTO = new List<ObjetivoDTO>();
			foreach (Objetivo objetivo in objetivos)
			{
				ObjetivoDTO objetivoDTO = new ObjetivoDTO()
				{
					Id = objetivo.Id,
					Titulo = objetivo.Titulo,
					MontoMaximo = objetivo.MontoMaximo,
					Categorias = Cambiar_CategoriasDTO(id, objetivo.Categorias),
					Token = objetivo.Token
				};
				objetivosDTO.Add(objetivoDTO);
			}
			return objetivosDTO;
		}

		private List<CategoriaDTO> Cambiar_CategoriasDTO(int id, List<Categoria> categorias)
		{
			List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();
			foreach (Categoria categoria in categorias)
			{
				CategoriaDTO categoriaDTO = new CategoriaDTO()
				{
					Id = categoria.Id,
					Nombre = categoria.Nombre,
					EstadoActivo = categoria.EstadoActivo,
					FechaCreacion = categoria.FechaCreacion,
					Tipo = Cambiar_TipoCategoria(categoria.Tipo)
				};
				categoriasDTO.Add(categoriaDTO);
			}
			return categoriasDTO;
		}

		private TipoCategoriaDTO Cambiar_TipoCategoria(TipoCategoria tipoCategoria)
		{
			return TipoCategoriaDTO.Costo;
		}
	}
}
