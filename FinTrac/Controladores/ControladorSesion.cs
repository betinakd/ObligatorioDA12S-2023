using LogicaNegocio;
using DTO;
using Excepcion;
using Dominio;

namespace Controlador
{
	public class ControladorSesion
	{
		EspacioLogica _espacioLogic;
		UsuarioLogica _usuarioLogic;


		public ControladorSesion(UsuarioLogica usuarioLogic, EspacioLogica espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public string ValidarInicioSesion(string correo, string contrasena)
		{
			string errorMsj = "";
			try
			{
				_usuarioLogic.UsuarioPorCorreoContrasena(correo, contrasena);
			}
			catch (LogicaNegocioUsuarioExcepcion ex)
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
				Usuario usuario = _usuarioLogic.UsuarioPorCorreoContrasena(correo, contrasena);
				usuarioDTO.Nombre = usuario.Nombre;
				usuarioDTO.Correo = usuario.Correo;
				usuarioDTO.Apellido = usuario.Apellido;
				usuarioDTO.Direccion = usuario.Direccion;
				usuarioDTO.Contrasena = usuario.Contrasena;
				usuarioDTO.IdEspacioPrincipal = usuario.IdEspacioPrincipal;
			}
			catch (LogicaNegocioUsuarioExcepcion ex)
			{
				usuarioDTO = null;
			}
			return usuarioDTO;
		}

		public EspacioDTO EspacioActual(int idEspacio)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(idEspacio);
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
