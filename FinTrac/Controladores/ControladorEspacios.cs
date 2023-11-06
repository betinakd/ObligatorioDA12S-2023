using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorEspacios
	{
		private UsuarioLogic _usuarioLogic;
		public ControladorEspacios(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
		}
	}
}
