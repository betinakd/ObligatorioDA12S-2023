using LogicaNegocio;
using Dominio;
using DTO;
using Excepcion;

namespace Controlador
{
	public class ControladorEspacios
	{
		private UsuarioLogica _usuarioLogic;
		private EspacioLogica _espacioLogic;

		public ControladorEspacios(UsuarioLogica usuarioLogic, EspacioLogica espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public string CrearEspacio(string correoAdmin, string nombre)
		{
			string errorMsj = "";
			Usuario admin = _usuarioLogic.EncontrarUsuario(correoAdmin);
			try
			{
				_espacioLogic.CrearEspacio(nombre, admin);
				errorMsj = "Espacio " + nombre + " creado con éxito.";
			}
			catch (DominioEspacioExcepcion ex)
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
				errorMsj = "Espacio Modificado con éxito.";
			}
			catch (DominioEspacioExcepcion e)
			{
				errorMsj = e.Message;
			}
			return errorMsj;
		}

		public List<EspacioDTO> EspaciosDeUsuario(string correo)
		{
			List<Espacio> espacios = _espacioLogic.EspaciosPorCorreo(correo);
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
