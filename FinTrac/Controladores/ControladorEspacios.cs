using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorEspacios
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorEspacios(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}
	}
}
