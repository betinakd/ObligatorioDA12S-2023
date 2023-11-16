using LogicaNegocio;
using Dominio;
using DTO;

namespace Controlador
{
	public class ControladorUsuarios
	{
		private UsuarioLogica _usuarioLogic;
		private EspacioLogica _espacioLogic;

		public ControladorUsuarios(UsuarioLogica usuarioLogic,EspacioLogica espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public UsuarioDTO DatosAdminEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(idEspacio);
			Usuario admin = _usuarioLogic.EncontrarUsuario(espacio.Admin.Correo);
			UsuarioDTO datos = new UsuarioDTO()
			{
				Nombre = admin.Nombre,
				Apellido = admin.Apellido,
				Correo = admin.Correo
			};
			return datos;
		}

		public List<UsuarioDTO> DatosUsuariosInvitadosEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(idEspacio);
			List<Usuario> usuarios = espacio.UsuariosInvitados;
			List<UsuarioDTO> datos = new List<UsuarioDTO>();

			foreach (Usuario usuario in usuarios)
			{
				datos.Add(new UsuarioDTO()
				{
					Nombre = usuario.Nombre,
					Apellido = usuario.Apellido,
					Correo = usuario.Correo
				});
			}
			return datos;
		}

		public List<UsuarioDTO> DatosUsuariosNoPresentesEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.EncontrarEspacio(idEspacio);
			List<Usuario> usuariosNoPresentes =_usuarioLogic.UsuariosNoPresentesEspacio(espacio);
			List<UsuarioDTO> datos = new List<UsuarioDTO>();
			foreach(Usuario usuario in usuariosNoPresentes)
			{
				datos.Add(new UsuarioDTO()
				{
					Nombre = usuario.Nombre,
					Apellido = usuario.Apellido,
					Correo = usuario.Correo
				});
			}
			return datos;
		}

		public void AgregarUsuarioAEspacio(int idEspacio, string correoUsuario)
		{
			Usuario usuario = _usuarioLogic.EncontrarUsuario(correoUsuario);
			_espacioLogic.AgregarUsuarioAEspacio(idEspacio, usuario);
		}

		public void EliminarUsuarioDeEspacio(int idEspacio, string correoUsuario)
		{
			Usuario usuario = _usuarioLogic.EncontrarUsuario(correoUsuario);
			_espacioLogic.EliminarUsuarioDeEspacio(idEspacio, usuario);
		}
	}
}
