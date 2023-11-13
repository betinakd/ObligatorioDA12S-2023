using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;
using EspacioReporte;
using System;

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

		public string CrearObjetivo(int id, ObjetivoDTO objetivoDTO)
		{
			string msjError = "";
			Espacio espacio = _objetivoLogic.FindEspacio(id);
			try
			{
				Objetivo nuevoObjetivo = new Objetivo()
				{
					Titulo = objetivoDTO.Titulo,
					MontoMaximo = objetivoDTO.MontoMaximo,
					Categorias = Cambiar_Categorias(id, objetivoDTO.Categorias),

				};
				espacio.AgregarObjetivo(nuevoObjetivo);
				_objetivoLogic.UpdateEspacio(espacio);
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
			Espacio espacio = _objetivoLogic.FindEspacio(id);
			Objetivo objetivo = Cambiar_A_Objetivo(id, objetivoDTO.Id);
			objetivo.Titulo = objetivoDTO.Titulo;
			objetivo.MontoMaximo = objetivoDTO.MontoMaximo;
			objetivo.Categorias = Cambiar_Categorias(id, objetivoDTO.Categorias);
			objetivo.Token = objetivoDTO.Token;
			_objetivoLogic.UpdateEspacio(espacio);
			return msjError;
		}

		private Objetivo Cambiar_A_Objetivo(int id, int idObjetivoDTO)
		{
			Objetivo objetivoResultado = null;
			foreach (Objetivo objetivo in _objetivoLogic.FindEspacio(id).Objetivos)
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
			Espacio espacio = _objetivoLogic.FindEspacio(id);
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

		public string NombreAdmin(int Id)
		{
			string nombreAdmin = "";
			Espacio espacio = _objetivoLogic.FindEspacio(Id);
			nombreAdmin = espacio.Admin.Nombre;
			return nombreAdmin;
		}
	}
}