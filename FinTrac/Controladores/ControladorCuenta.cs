using DTO;
using Domain;
using Excepcion;
using BussinesLogic;

namespace Controlador
{
	public class ControladorCuenta
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;
		public ControladorCuenta(UsuarioLogic usuarioLogic,EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}
	}
}
