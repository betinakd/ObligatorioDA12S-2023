using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorRegistro
	{
		private UsuarioLogic _usuarioLogic;

		public ControladorRegistro(UsuarioLogic usuarioLogic)
		{
			_usuarioLogic = usuarioLogic;
		}

	}
}
