using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;
using EspacioReporte;

namespace Controlador
{
	public class ControladorObjetivos
	{
		private EspacioLogic _espacioLogic;

		public ControladorObjetivos(EspacioLogic objetivoLogic)
		{
			_espacioLogic = objetivoLogic;
		}

		public List<ObjetivoDTO> ObjetivosDeEspacio(int id)
		{
			Espacio espacio = _espacioLogic.FindEspacio(id);
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

		public string CrearObjetivo(int id, ObjetivoDTO objetivoDTO)
		{
			string msjError = "";
			Espacio espacio = _espacioLogic.FindEspacio(id);
			try
			{
				Objetivo nuevoObjetivo = new Objetivo()
				{
					Titulo = objetivoDTO.Titulo,
					MontoMaximo = objetivoDTO.MontoMaximo,
					Categorias = Cambiar_Categorias(id, objetivoDTO.Categorias),

				};
				espacio.AgregarObjetivo(nuevoObjetivo);
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DomainEspacioException e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarObjetivo(int id, ObjetivoDTO objetivoDTO)
		{
			string msjError = "";
			Espacio espacio = _espacioLogic.FindEspacio(id);
			Objetivo objetivo = Cambiar_A_Objetivo(id, objetivoDTO.Id);
			objetivo.Titulo = objetivoDTO.Titulo;
			objetivo.MontoMaximo = objetivoDTO.MontoMaximo;
			objetivo.Categorias = Cambiar_Categorias(id, objetivoDTO.Categorias);
			objetivo.Token = objetivoDTO.Token;
			_espacioLogic.UpdateEspacio(espacio);
			return msjError;
		}

		private Objetivo Cambiar_A_Objetivo(int id, int idObjetivoDTO)
		{
			Objetivo objetivoResultado = null;
			foreach (Objetivo objetivo in _espacioLogic.FindEspacio(id).Objetivos)
			{
				if (objetivo.Id == idObjetivoDTO)
				{
					objetivoResultado = objetivo;
				}
			}
			return objetivoResultado;
		}

		private List<Categoria> Cambiar_Categorias(int id, List<CategoriaDTO> categoriasDTO)
		{
			Espacio espacio = _espacioLogic.FindEspacio(id);
			List<Categoria> categoriasDeEspacio = espacio.Categorias;
			List<Categoria> categorias = new List<Categoria>();
			foreach (Categoria categoria in categoriasDeEspacio)
			{
				if (categoriasDTO.Any(c => c.Id == categoria.Id))
				{
					categorias.Add(categoria);
				}
			}
			return categorias;
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

		public string EspacioActual(int Id)
		{
			string nombreEspacio = "";
			Espacio espacio = _espacioLogic.FindEspacio(Id);
			nombreEspacio = espacio.Nombre;
			return nombreEspacio;
		}

		public double ObjetivosDeGastos(int id, int objetivoDTO)
		{
			Espacio espacio = _espacioLogic.FindEspacio(id);
			Objetivo objetivo = Cambiar_A_Objetivo(id, objetivoDTO);
			double objetivosDeGasto = espacio.GastosDeObjetivo(objetivo);
			return objetivosDeGasto;
		}
	}
}