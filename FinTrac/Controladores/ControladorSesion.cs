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


	}
}
