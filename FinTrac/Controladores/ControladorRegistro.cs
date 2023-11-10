using BussinesLogic;
using Domain;
using DTO;

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

		public void CrearEspacioPrincipal(string correo)
		{
			Usuario admin = _usuarioLogic.FindUsuario(correo);
			string nombre = "Principal " + admin.Nombre;
			_espacioLogic.CrearEspacio(nombre, admin);
		}

		public void RegistrarUsuario(string correo, string nombre, string apellido, string contrasena, string direccion, int idEspacioPrincipal)
		{
			_usuarioLogic.CrearUsuario(correo, nombre, apellido, contrasena, direccion, idEspacioPrincipal);
			CrearEspacioPrincipal(correo);
		}

		public void RegistrarUsuario(UsuarioDTO usuarioDTO)
		{
			Usuario usuario = new Usuario()
			{
				Correo = usuarioDTO.Correo,
				Nombre = usuarioDTO.Nombre,
				Apellido = usuarioDTO.Apellido,
				Contrasena = usuarioDTO.Contrasena,
				Direccion = usuarioDTO.Direccion
			};
			int idEspacioPrincipal = _espacioLogic.EspacioMayorId() + 1;
			_usuarioLogic.CrearUsuario(usuarioDTO.Correo, usuarioDTO.Nombre, usuarioDTO.Apellido, usuarioDTO.Contrasena, usuarioDTO.Direccion, idEspacioPrincipal);
			CrearEspacioPrincipal(usuarioDTO.Correo);
		}
	}
}
