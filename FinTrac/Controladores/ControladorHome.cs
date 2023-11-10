using BussinesLogic;
using Domain;
using DTO;

namespace Controlador
{
	public class ControladorHome
	{
		private UsuarioLogic _usuarioLogic;
		public UsuarioDTO Usuario { get; set; }

		public ControladorHome(UsuarioLogic usuarioLogic, string correo)
		{
			_usuarioLogic = usuarioLogic;
			Usuario usuario = usuarioLogic.FindUsuario(correo);
			Usuario = new UsuarioDTO()
			{
				Correo = usuario.Correo,
				Apellido = usuario.Apellido,
				Nombre = usuario.Nombre,
				Direccion = usuario.Direccion,
				Contrasena = usuario.Contrasena
			};
		}

		public void ModificarNombre(string nombre)
		{
			_usuarioLogic.ModificarNombre(Usuario.Correo, nombre);
			Usuario.Nombre = nombre;
		}

		public void ModificarApellido(string apellido)
		{
			_usuarioLogic.ModificarApellido(Usuario.Correo, apellido);
			Usuario.Apellido = apellido;
		}

		public void ModificarContrasena(string contrasena)
		{
			_usuarioLogic.ModificarContrasena(Usuario.Correo, contrasena);
			Usuario.Contrasena = contrasena;
		}

		public void ModificarDireccion(string direccion)
		{
			_usuarioLogic.ModificarDireccion(Usuario.Correo, direccion);
			Usuario.Direccion = direccion;
		}
	}
}