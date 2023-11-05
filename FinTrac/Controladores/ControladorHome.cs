using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorHome
	{
		public UsuarioLogic UsuarioLogic { get; set; }
		public string Correo { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Direccion { get; set; }
		public string Contrasena { get; set; }	

		public ControladorHome(UsuarioLogic usuarioLogic, string correo)
		{
			Usuario usuario = usuarioLogic.FindUsuario(correo);
			UsuarioLogic = usuarioLogic;
			Correo = correo;
			Apellido = usuario.Apellido;
			Nombre = usuario.Nombre;
		}
	}
}