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

		[TestMethod]
		public void Find_EspacioPorID()
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
					Contrasena = "HOLAhola123",
					Correo = "hola@gmail.com",
					Direccion = "Direccion1"
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
					Contrasena = "HOLAhola123",
					Correo = "hola2@gmail.com",
					Direccion = "Direccion2"
				}
			};
			_repository.Add(espacio1);
			_repository.Add(espacio2);
			var espacio = _repository.Find(e => e.Id == 1);
			Assert.AreEqual(espacio1, espacio);
			Assert.AreEqual(espacio1.Id, espacio.Id);
		}

		[TestMethod]
		public void Find_EspacioNull()
		{
			var espacio = _repository.Find(e => e.Id == 1);
			Assert.AreEqual(null, espacio);
		}

		[TestMethod]
		public void FindAll_Devuelve_Espacios_Con_Administrador()
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
					Direccion = "Dirección de ejemplo"
				}
			};
			_repository.Add(espacio);
			var espacios = _repository.FindAll().ToList();
			var espacioDevuelto = espacios.FirstOrDefault();
			Assert.AreEqual(espacioDevuelto.Admin, espacio.Admin);
		}

		[TestMethod]
		public void FindAll_Devuelve_Espacios_Con_Categorias()
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
					Direccion = "Dirección de ejemplo"
				}
			};
			var categoria = new Categoria
			{
				Id = 1,
				Nombre = "Categoria 1",
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				FechaCreacion = DateTime.Now,
			};
			espacio.Categorias.Add(categoria);
			_repository.Add(espacio);
			var espacios = _repository.FindAll().ToList();
			var espacioDevuelto = espacios.FirstOrDefault();
			Assert.AreEqual(espacioDevuelto.Categorias.FirstOrDefault(), espacio.Categorias.FirstOrDefault());
		}

		[TestMethod]
		public void FindAll_Devuelve_Espacios_Con_Transacciones()
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
					Direccion = "Dirección de ejemplo"
				}
			};
			var categoria = new Categoria
			{
				Id = 1,
				Nombre = "Categoria 1",
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				FechaCreacion = DateTime.Now,
			};
			espacio.Categorias.Add(categoria);
			var cuenta = new Ahorro
			{
				Id = 1,
				Nombre = "Cuenta 1",
				Moneda = TipoCambiario.PesosUruguayos,
				Monto = 1000,
			};
			espacio.Cuentas.Add(cuenta);
			var transaccion = new Transaccion
			{
				Id = 1,
				Titulo = "Transaccion 1",
				FechaTransaccion = DateTime.Now,
				Monto = 100,
				Moneda = TipoCambiario.PesosUruguayos,
				CuentaMonetaria = cuenta,
				CategoriaTransaccion = categoria
			};
			espacio.Transacciones.Add(transaccion);
			_repository.Add(espacio);
			var espacios = _repository.FindAll().ToList();
			var espacioDevuelto = espacios.FirstOrDefault();
			Assert.AreEqual(espacioDevuelto.Transacciones.FirstOrDefault(), espacio.Transacciones.FirstOrDefault());
		}
	}
}
