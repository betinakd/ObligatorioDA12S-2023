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
			List<CambioDTO> cambiosDTO = new List<CambioDTO>();
			return cambiosDTO;
		}
	}
}
