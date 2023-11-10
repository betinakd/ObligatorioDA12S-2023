using BussinesLogic;
using Domain;
using DTO;
using Excepcion;

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

		public string CrearEspacio(string correoAdmin, string nombre)
		{
			string errorMsj = "";
			Usuario admin = _usuarioLogic.FindUsuario(correoAdmin);
			try
			{
				_espacioLogic.CrearEspacio(nombre, admin);
				errorMsj = "Espacio " + nombre + " Modificado con éxito.";
			}
			catch (DomainEspacioException ex)
			{
				errorMsj = ex.Message;
			}
			return errorMsj;
		}

		public string ModificarNombreEspacio(int espacioId, string nuevoNombre)
		{
			string errorMsj = "";
			try
			{
				_espacioLogic.ModificarNombreEspacio(espacioId, nuevoNombre);
				errorMsj = "Espacio " + nuevoNombre + " Modificado con éxito.";
			}
			catch (DomainEspacioException e)
			{
				errorMsj = e.Message;
			}
			return errorMsj;
		}

		public List<EspacioDTO> EspaciosDeUsuario(string correo)
		{
			List<Espacio> espacios = _espacioLogic.EspaciosByCorreo(correo);
			List<EspacioDTO> nombresEspacios = new List<EspacioDTO>();

			foreach (Espacio espacio in espacios)
			{
				var admin = new UsuarioDTO()
				{
					Nombre = espacio.Admin.Nombre,
					Apellido = espacio.Admin.Apellido,
					Correo = espacio.Admin.Correo,
				};

				nombresEspacios.Add(
					new EspacioDTO()
					{
						Id = espacio.Id,
						Nombre = espacio.Nombre,
						Admin = admin,
					}
				);
			}
			return nombresEspacios;
		}






	}
}
