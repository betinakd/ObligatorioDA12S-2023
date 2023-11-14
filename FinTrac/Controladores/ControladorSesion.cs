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

				_usuarioLogic.UsuarioByCorreoContrasena(correo, contrasena);

			return errorMsj;
		}


	}
}
