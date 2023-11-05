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

		public void RegistrarUsuario(string correo, string nombre, string apellido, string contrasena, string direccion)
		{
			_usuarioLogic.CrearUsuario(correo, nombre, apellido, contrasena, direccion);
		}
	}
}
