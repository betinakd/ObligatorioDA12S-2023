using Repository;
using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTest
{
	[TestClass]
	public class EspacioRepositorySqlTest
	{
		private EspacioMemoryRepository _repository;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void SetUp()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new EspacioMemoryRepository(_context);
		}

		[TestCleanup]
		public void CleanUp()
		{
			_context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void FindAll_Trae_Todos_Los_Espacios()
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
			_context.Espacios.Add(espacio);
			_context.SaveChanges();

			var espacios = _repository.FindAll();
			Assert.IsTrue(espacios.Contains(espacio));
			Assert.AreEqual(1, espacios.Count);
		}

		[TestMethod]
		public void Add_Espacio_Se_Guarda_Correctamente()
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
			_repository.Add(espacio);
			var espacios = _repository.FindAll();
			Assert.IsTrue(espacios.Contains(espacio));
			Assert.AreEqual(1, espacios.Count);
		}

		[TestMethod]
		public void Update_Espacio_Se_Actualiza_Correctamente()
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
					Contrasena = "HOLAhola123",
					Correo = "hola@gmail.com",
					Direccion = "Direccion1"
				}
			};
			_repository.Add(espacio);
			espacio.Nombre = "Espacio 2";
			_repository.Update(espacio);
			var espacios = _repository.FindAll();
			Assert.IsTrue(espacios.Contains(espacio));
		}

	}
}
