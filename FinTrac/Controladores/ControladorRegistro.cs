using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorRegistro
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorRegistro(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public void RegistrarUsuario(string correo, string nombre, string apellido, string contrasena, string direccion)
		{
			_usuarioLogic.CrearUsuario(correo, nombre, apellido, contrasena, direccion);
		}
	}
}
