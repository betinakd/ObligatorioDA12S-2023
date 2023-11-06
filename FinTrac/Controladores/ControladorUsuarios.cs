using BussinesLogic;
using Domain;


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

		public string[] DatosAdminEspacio(int idEspacio) { 
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			Usuario admin = _usuarioLogic.FindUsuario(espacio.Admin.Correo);
			string[] datos = { admin.Nombre, admin.Apellido, admin.Correo};
			return datos;
		}

		public string[,] DatosUsuariosInvitadosEspacio(int idEspacio)
		{
			Espacio espacio = _espacioLogic.FindEspacio(idEspacio);
			List<Usuario> usuarios = espacio.UsuariosInvitados;
			string[,] datos = new string[ usuarios.Count , 3];

			for (int i = 0; i < usuarios.Count; i++)
			{
				datos[i, 0] = usuarios[i].Nombre;
				datos[i, 1] = usuarios[i].Apellido;
				datos[i, 2] = usuarios[i].Correo;
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
	}
}
