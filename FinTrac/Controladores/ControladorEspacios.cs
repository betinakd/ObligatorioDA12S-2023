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

		public void CrearEspacio(string correoAdmin, string nombre) { 
			Usuario admin = _usuarioLogic.FindUsuario(correoAdmin);
			_espacioLogic.CrearEspacio(nombre, admin);
		}
	}
}
