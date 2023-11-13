using BussinesLogic;
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
