using BussinesLogic;
using Excepcion;
using Domain;
using DTO;

namespace Controlador
{
	public class ControladorRegistro
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorRegistro(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public void CrearEspacioPrincipal(string correo)
		{
			Usuario admin = _usuarioLogic.FindUsuario(correo);
			string nombre = "Principal " + admin.Nombre;
			_espacioLogic.CrearEspacio(nombre, admin);
		}

		public string RegistrarUsuario(UsuarioDTO usuarioDTO)
		{
			string msjError = "";
			int idEspacioPrincipal = _espacioLogic.EspacioMayorId() + 1;
			try
			{
				Usuario usuario = new Usuario()
				{
					Correo = usuarioDTO.Correo,
					Nombre = usuarioDTO.Nombre,
					Apellido = usuarioDTO.Apellido,
					Contrasena = usuarioDTO.Contrasena,
					Direccion = usuarioDTO.Direccion,
					IdEspacioPrincipal = idEspacioPrincipal
				};
				_usuarioLogic.CrearUsuario(usuario);
				CrearEspacioPrincipal(usuarioDTO.Correo);
			}
			catch (BussinesLogicUsuarioException e)
			{
				msjError = e.Message;
			}
			catch (DomainUsuarioException e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public bool RegistradoConExito(UsuarioDTO usuario)
		{
			Usuario usuarioEncontrado = _usuarioLogic.FindUsuario(usuario.Correo);

			usuario.IdEspacioPrincipal = usuarioEncontrado.IdEspacioPrincipal;
			return true;
		}
	}
}
