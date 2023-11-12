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
			return objetivosDTO;
		}
	}
}
