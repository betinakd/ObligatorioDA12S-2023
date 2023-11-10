using BussinesLogic;
using Domain;
using DTO;
using System.Collections;
using System.Collections.Generic;

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

		public void CrearEspacio(string correoAdmin, string nombre)
		{
			Usuario admin = _usuarioLogic.FindUsuario(correoAdmin);
			_espacioLogic.CrearEspacio(nombre, admin);
		}

		public string ModificarNombreEspacio(int espacioId, string nuevoNombre)
		{
			string errorMsj = "";
			_espacioLogic.ModificarNombreEspacio(espacioId, nuevoNombre);
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
