using BussinesLogic;
using Domain;

namespace Controlador
{
	public class ControladorEspacios
	{
		private UsuarioLogic _usuarioLogic;
		private EspacioLogic _espacioLogic;

		public ControladorEspacios(UsuarioLogic usuarioLogic, EspacioLogic espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public void CrearEspacio(string correoAdmin, string nombre) { 
			Usuario admin = _usuarioLogic.FindUsuario(correoAdmin);
			_espacioLogic.CrearEspacio(nombre, admin);
		}

		public void ModificarNombreEspacio(int espacioId, string nuevoNombre)
		{
			_espacioLogic.ModificarNombreEspacio(espacioId, nuevoNombre);
		}

		public string[,] EspaciosDeUsuario(string correo)
		{
			List<Espacio> espacios = _espacioLogic.EspaciosByCorreo(correo);
			string[,] nombresEspacios = new string[espacios.Count, 3];

			for (int i = 0; i < espacios.Count; i++)
			{
				nombresEspacios[i, 0] = espacios[i].Nombre;
				if (espacios[i].Admin.Correo == correo)
				{
					nombresEspacios[i, 1] = "Administrador";
				}
				else
				{
					nombresEspacios[i, 1] = "Invitado";
				}
				nombresEspacios[i, 2] = espacios[i].Id.ToString();
			}
			return nombresEspacios;
		}
	}
}
