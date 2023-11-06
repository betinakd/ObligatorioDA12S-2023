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
	}
}
