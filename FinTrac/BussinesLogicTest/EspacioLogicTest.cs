using Excepcion;
using Domain;
using Repository;
using BussinesLogic;
using Microsoft.EntityFrameworkCore;

namespace BussinesLogicTest
{
	[TestClass]
	public class EspacioLogicTest
	{
		private IRepository<Espacio> _repository;
		private EspacioLogic espacioLogic;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void Setup()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new EspacioMemoryRepository(_context);
			espacioLogic = new EspacioLogic(_repository);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_context = null;
			_repository = null;
			espacioLogic = null;
		}

		[TestMethod]
		public void Nuevo_EspacioLogic()
		{
			Assert.IsNotNull(espacioLogic);
		}

		[TestMethod]
		public void Agregar_Espacio()
		{
			var espacio1 = new Espacio
			{
				Id = 1,
				Nombre = "Espacio 1",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 1,
					Nombre = "Usuario1",
					Apellido = "Apellido1",
					Correo = "usuario@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion1",
				}
			};
			var espacio2 = new Espacio
			{
				Id = 2,
				Nombre = "Espacio 2",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 2,
					Nombre = "Usuario2",
					Apellido = "Apellido2",
					Correo = "usuario2@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion2",
				}
			};
			espacioLogic.AddEspacio(espacio1);
			Assert.IsTrue(_repository.FindAll().Contains(espacio1));
		}

		[TestMethod]
		[ExpectedException(typeof(BusinessLogicEspacioException))]
		public void Agregar_Espacio_Duplicado()
		{
			var espacio1 = new Espacio
			{
				Id = 1,
				Nombre = "Espacio 1",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 1,
					Nombre = "Usuario1",
					Apellido = "Apellido1",
					Correo = "usuario@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion1",
				}
			};
			var espacio2 = new Espacio
			{
				Id = 2,
				Nombre = "Espacio 2",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 2,
					Nombre = "Usuario2",
					Apellido = "Apellido2",
					Correo = "usuario2@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion2",
				}
			};
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio1);
		}

		[TestMethod]
		public void Buscar_Todos_Espacios()
		{
			var espacio1 = new Espacio
			{
				Id = 1,
				Nombre = "Espacio 1",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 1,
					Nombre = "Usuario1",
					Apellido = "Apellido1",
					Correo = "usuario@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion1",
				}
			};
			var espacio2 = new Espacio
			{
				Id = 2,
				Nombre = "Espacio 2",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 2,
					Nombre = "Usuario2",
					Apellido = "Apellido2",
					Correo = "usuario2@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion2",
				}
			};
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio1));
			Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio2));
		}

		[TestMethod]
		public void Buscar_Espacio()
		{
			var espacio = new Espacio
			{
				Id = 1,
				Nombre = "Espacio 1",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 1,
					Nombre = "Usuario1",
					Apellido = "Apellido1",
					Correo = "usuario@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion1",
				}
			};
			espacioLogic.AddEspacio(espacio);

			Espacio resultado1 = espacioLogic.FindEspacio(espacio.Id);

			Assert.AreEqual(espacio.Nombre, resultado1.Nombre);
		}

		[TestMethod]
		public void Retorna_Lista_Espacios_Recibiendo_Correo_Valido()
		{
			var espacio1 = new Espacio
			{
				Id = 1,
				Nombre = "Espacio 1",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 1,
					Nombre = "Usuario1",
					Apellido = "Apellido1",
					Correo = "usuario@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion1",
				}
			};
			var espacio2 = new Espacio
			{
				Id = 2,
				Nombre = "Espacio 2",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 2,
					Nombre = "Usuario2",
					Apellido = "Apellido2",
					Correo = "usuario2@gmail.com",
					Contrasena = "HOLAhola123",
					Direccion = "Direccion2",
				}
			};
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			List<Espacio> espacios = espacioLogic.EspaciosByCorreo("usuario2@gmail.com");
			Assert.IsTrue(espacios.Count == 1);
		}
	}
}
