using BussinesLogic;
using Domain;
using DTO;
using Excepcion;

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

		public string ModificarNombre(string nombre)
		{
			string errorMsj = "Sus datos han sido modificados correctamente.";
			try
			{
				_usuarioLogic.ModificarNombre(Usuario.Correo, nombre);
			}
			catch (DomainUsuarioException ex)
			{
				Usuario.Nombre = nombre;
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarApellido(string apellido)
		{
			string errorMsj = "Sus datos han sido modificados correctamente.";
			try
			{
				_usuarioLogic.ModificarApellido(Usuario.Correo, apellido);
			}
			catch (DomainUsuarioException ex)
			{
				Usuario.Apellido = apellido;
				errorMsj = ex.Message;
			}
			return errorMsj;
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