﻿using BussinesLogic;
using DTO;
using DTO.EnumsDTO;
using Domain;
using Excepcion;

namespace Controlador
{
	public class ControladorCambios
	{
		private EspacioLogic _cambioLogic;

		public ControladorCambios(EspacioLogic cambioLogic)
		{
			_cambioLogic = cambioLogic;
		}

		public List<CambioDTO> CambiosDeEspacio(int id)
		{
			Espacio espacio = _cambioLogic.FindEspacio(id);
			List<Cambio> cambios = espacio.Cambios;
			List<CambioDTO> cambiosDTO = new List<CambioDTO>();
			foreach (Cambio cambio in cambios)
			{
				CambioDTO cambioDTO = new CambioDTO()
				{
					Id = cambio.Id,
					Moneda = Cambiar_TipoCambiarioDTO(cambio.Moneda),
					FechaDeCambio = cambio.FechaDeCambio,
					Pesos = cambio.Pesos
				};
				cambiosDTO.Add(cambioDTO);
			}
			return cambiosDTO;
		}

		public string CrearCambio(int id, CambioDTO cambioDTO)
		{
			string msjError = "";
			Espacio espacio = _cambioLogic.FindEspacio(id);
			TipoCambiario tipo = Cambiar_TipoCambiario(cambioDTO.Moneda);
			try
			{
				Cambio nuevoCambio = new Cambio()
				{
					Moneda = tipo,
					FechaDeCambio = cambioDTO.FechaDeCambio,
					Pesos = cambioDTO.Pesos
				};
				espacio.AgregarCambio(nuevoCambio);
				_cambioLogic.UpdateEspacio(espacio);
			}
			catch (DomainEspacioException e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarCambio(int id, CambioDTO cambioDTO, double valor)
		{
			string msjError = "";
			Espacio espacio = _cambioLogic.FindEspacio(id);
			Cambio cambio = Cambiar_A_Cambio(id, cambioDTO.Id);
			cambio.Pesos = valor;
			_cambioLogic.UpdateEspacio(espacio);
			return msjError;
		}

		private Cambio Cambiar_A_Cambio(int idEspacio, int idCambio)
		{
			Cambio cambioResultado = null;
			Espacio espacio = _cambioLogic.FindEspacio(idEspacio);
			List<Cambio> cambios = espacio.Cambios;
			foreach (Cambio cambio in cambios)
			{
				if (cambio.Id == idCambio)
				{
					cambioResultado = cambio;
				}
			}
			return cambioResultado;
		}

		private TipoCambiario Cambiar_TipoCambiario(TipoCambiarioDTO tipoDTO)
		{
			TipoCambiario tipoResulatado = TipoCambiario.Dolar;
			if (tipoDTO != TipoCambiarioDTO.PesosUruguayos)
			{
				foreach (TipoCambiario tipo in Enum.GetValues(typeof(TipoCambiario)))
				{
					if (tipo.ToString() == tipoDTO.ToString())
					{
						tipoResulatado = tipo;
					}
				}
			}
			return tipoResulatado;
		}

		private TipoCambiarioDTO Cambiar_TipoCambiarioDTO(TipoCambiario tipo)
		{
			TipoCambiarioDTO tipoResulatado = TipoCambiarioDTO.Dolar;
			if (tipo != TipoCambiario.PesosUruguayos)
			{
				foreach (TipoCambiarioDTO tipoDTO in Enum.GetValues(typeof(TipoCambiarioDTO)))
				{
					if (tipoDTO.ToString() == tipo.ToString())
					{
						tipoResulatado = tipoDTO;
					}
				}
			}
			return tipoResulatado;
		}
	}
}