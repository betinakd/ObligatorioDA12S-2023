﻿using BussinesLogic;
using Controlador;
using Domain;
using Excepcion;
using Repository;

namespace ControladorTest
{
	[TestClass]
	public class ControladorEspaciosTest
	{
		private IRepository<Usuario> _repositorioUsuario;
		private UsuarioLogic _usuarioLogic;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		private IRepository<Espacio> _repositorioEspacio;
		private EspacioLogic _espacioLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repositorioUsuario = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repositorioUsuario);
			_repositorioEspacio = new EspacioMemoryRepository(_context);
			_espacioLogic = new EspacioLogic(_repositorioEspacio);

			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			var usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			_usuarioLogic.AddUsuario(usuario1);
			_usuarioLogic.AddUsuario(usuario2);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorEspacios_Tiene_UsuarioLogic()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorEspacios_Tiene_EspacioLogic()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void CrearEspacio_Lanza_Excepcion_Crear_Espacio_CorreoAdmin_No_Existe()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			controladorTest.CrearEspacio("test@gmail.com", "EspacioTest");
		}

		[TestMethod]
		public void CrearEspacio_Crea_Espacio_Correctamente_Con_Admin_Existente()
		{
			ControladorEspacios controladorTest = new ControladorEspacios(_usuarioLogic, _espacioLogic);
			Usuario creadorEspacio = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(creadorEspacio);
			controladorTest.CrearEspacio("test@gmail.com", "Espacio Test");
			Espacio espacioCreado = _espacioLogic.FindEspacio(1);
			Assert.AreEqual("Espacio Test", espacioCreado.Nombre);
			Assert.AreEqual(creadorEspacio, espacioCreado.Admin);
		}
	}
}