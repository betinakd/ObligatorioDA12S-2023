﻿using BussinesLogic;
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
			Direccion = usuario.Direccion;
			Contrasena = usuario.Contrasena;
		}

		public void ModificarNombre(string nombre)
		{
			UsuarioLogic.ModificarNombre(Correo, nombre);
			Nombre = nombre;
		}

		public void ModificarApellido(string apellido)
		{
			UsuarioLogic.ModificarApellido(Correo, apellido);
			Apellido = apellido;
		}

		public void ModificarContrasena(string contrasena)
		{
			UsuarioLogic.ModificarContrasena(Correo, contrasena);
			Contrasena = contrasena;
		}

		public void ModificarDireccion(string direccion)
		{
			UsuarioLogic.ModificarDireccion(Correo, direccion);
			Direccion = direccion;
		}
	}
}