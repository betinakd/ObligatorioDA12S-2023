using BussinesLogic;
using DTO;
using Excepcion;
using Domain;

namespace Controlador
{
	public class ControladorSesion
	{
		EspacioLogic _espacioLogic;
		UsuarioLogic _usuarioLogic;


		public ControladorSesion(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public string ValidarInicioSesion(string correo, string contrasena)
		{
			string errorMsj = "";
			try
			{
				_usuarioLogic.UsuarioByCorreoContrasena(correo, contrasena);
			}
			catch (BussinesLogicUsuarioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public UsuarioDTO DarUsuarioLogeado(string correo, string contrasena)
		{
			UsuarioDTO usuarioDTO = new UsuarioDTO();
			try
			{
				Usuario usuario = _usuarioLogic.UsuarioByCorreoContrasena(correo, contrasena);
				usuarioDTO.Nombre = usuario.Nombre;
				usuarioDTO.Correo = usuario.Correo;
				usuarioDTO.Apellido = usuario.Apellido;
				usuarioDTO.Direccion = usuario.Direccion;
				usuarioDTO.Contrasena = usuario.Contrasena;
				usuarioDTO.IdEspacioPrincipal = usuario.IdEspacioPrincipal;
			}
			catch (BussinesLogicUsuarioException ex)
			{
			}
			return usuarioDTO;
		}

		public EspacioDTO EspacioActual(int idEspacio)
		{
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			UsuarioDTO adminDTO = new UsuarioDTO()
			{
				Nombre = espacio.Admin.Nombre,
				Apellido = espacio.Admin.Apellido,
				Correo = espacio.Admin.Correo,
				Direccion = espacio.Admin.Direccion,
				Contrasena = espacio.Admin.Contrasena
			};
			EspacioDTO espacioDTO = new EspacioDTO()
			{
				Id = espacio.Id,
				Nombre = espacio.Nombre,
				Admin = adminDTO
			};
			return espacioDTO;
		}
	}
}
