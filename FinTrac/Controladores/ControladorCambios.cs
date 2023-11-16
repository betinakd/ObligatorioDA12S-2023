using LogicaNegocio;
using DTO;
using DTO.EnumsDTO;
using Dominio;
using Excepcion;

namespace Controlador
{
	public class ControladorCambios
	{
		private EspacioLogica _espacioLogic;

		public ControladorCambios(EspacioLogica espacioLogic)
		{
			_espacioLogic = espacioLogic;
		}

		public List<CambioDTO> CambiosDeEspacio(int id)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(id);
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
			Espacio espacio = _espacioLogic.EncontrarEspacio(id);
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
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public string ModificarCambio(int id, CambioDTO cambioDTO)
		{
			string msjError = "";
			Espacio espacio = _espacioLogic.EncontrarEspacio(id);
			Cambio cambio = Cambiar_A_Cambio(id, cambioDTO.Id);
			try
			{
				cambio.Pesos = cambioDTO.Pesos;
				_espacioLogic.UpdateEspacio(espacio);
			}
			catch (DominioEspacioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		private Cambio Cambiar_A_Cambio(int idEspacio, int idCambio)
		{
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			List<Cambio> cambios = espacio.Cambios;
			Cambio cambio = cambios.Find(c => c.Id == idCambio);
			return cambio;
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
