using BussinesLogic;
using Domain;
using DTO;

namespace Controlador
{
	public class ControladorUsuarios
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorUsuarios(UsuarioLogic usuarioLogic,EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public UsuarioDTO DatosAdminEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			Usuario admin = _usuarioLogic.FindUsuario(espacio.Admin.Correo);
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
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
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

		public string[,] DatosUsuariosNoPresentesEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			List<Usuario> usuariosNoPresentes =_usuarioLogic.UsuariosNoPresentesEspacio(espacio);
			string[,] datos = new string[usuariosNoPresentes.Count, 3];
			for(int i=0; i<usuariosNoPresentes.Count; i++)
			{
				datos[i, 0] = usuariosNoPresentes[i].Nombre;
				datos[i, 1] = usuariosNoPresentes[i].Apellido;
				datos[i, 2] = usuariosNoPresentes[i].Correo;
			}
			return datos;
		}

		public void AgregarUsuarioAEspacio(int idEspacio, string correoUsuario)
		{
			Usuario usuario = _usuarioLogic.FindUsuario(correoUsuario);
			_espacioLogic.AgregarUsuarioAEspacio(idEspacio, usuario);
		}

		public void EliminarUsuarioDeEspacio(int idEspacio, string correoUsuario)
		{
			Usuario usuario = _usuarioLogic.FindUsuario(correoUsuario);
			_espacioLogic.EliminarUsuarioDeEspacio(idEspacio, usuario);
		}
	}
}
