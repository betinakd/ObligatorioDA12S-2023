using BussinesLogic;
using Domain;


namespace Controlador
{
	public class ControladorUsuarios
	{
		private UsuarioLogic _usuarioLogic;

		public ControladorUsuarios(UsuarioLogic usuarioLogic)
		{
			_usuarioLogic = usuarioLogic;
		}
	}
}
