using BussinesLogic;
using DTO;
using Excepcion;
using Domain;

namespace Controlador
{
	public class ControladorHome
	{
		private UsuarioLogic _usuarioLogic;

		public ControladorHome(UsuarioLogic usuarioLogic)
		{
			_usuarioLogic = usuarioLogic;
		}

		public UsuarioDTO DarUsuarioDTO(string correo) 
		{
			Usuario usuario = _usuarioLogic.FindUsuario(correo);

			UsuarioDTO usuarioDTO = new UsuarioDTO
			{
				Nombre = usuario.Nombre,
				Correo = usuario.Correo,
				Apellido = usuario.Apellido,
				Direccion = usuario.Direccion,
				Contrasena = usuario.Contrasena
			};
			return usuarioDTO;
		}

		public string ModificarNombre(string nombre, string correo)
		{
			string errorMsj = "Su nombre ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarNombre(correo, nombre);
			}
			catch (DomainUsuarioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarApellido(string apellido, string correo)
		{
			string errorMsj = "Su apellido ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarApellido(correo, apellido);
			}
			catch (DomainUsuarioException ex)
			{

				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarContrasena(string contrasena, string correo)
		{
			string errorMsj = "Su contraseña ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarContrasena(correo, contrasena);
			}
			catch (DomainUsuarioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarDireccion(string direccion, string correo)
		{
			string errorMsj = "Su dirección ha sido modificado correctamente.";

			_usuarioLogic.ModificarDireccion(correo, direccion);

			return errorMsj;
		}
	}
}