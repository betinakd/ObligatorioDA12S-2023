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
			string errorMsj = "Su nombre ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarNombre(Usuario.Correo, nombre);
				Usuario.Nombre = nombre;
			}
			catch (DomainUsuarioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarApellido(string apellido)
		{
			string errorMsj = "Su apellido ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarApellido(Usuario.Correo, apellido);
				Usuario.Apellido = apellido;
			}
			catch (DomainUsuarioException ex)
			{

				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarContrasena(string contrasena)
		{
			string errorMsj = "Su contraseña ha sido modificado correctamente.";
			try
			{
				_usuarioLogic.ModificarContrasena(Usuario.Correo, contrasena);
				Usuario.Contrasena = contrasena;
			}
			catch (DomainUsuarioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarDireccion(string direccion)
		{
			string errorMsj = "Su dirección ha sido modificado correctamente.";

			_usuarioLogic.ModificarDireccion(Usuario.Correo, direccion);
			Usuario.Direccion = direccion;

			return errorMsj;
		}
	}
}