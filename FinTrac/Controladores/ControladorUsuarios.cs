using BussinesLogic;
using Domain;


namespace Controlador
{
	public class ControladorUsuarios
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorUsuarios(UsuarioLogic usuarioLogic,EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}
	}
}
