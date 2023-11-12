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
	}
}
