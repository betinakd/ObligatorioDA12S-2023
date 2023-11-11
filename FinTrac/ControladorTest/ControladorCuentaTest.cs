using DTO;
using Controlador;
using BussinesLogic;
using Domain;
using Repository;
using System.Linq;

namespace ControladorTest
{
	[TestClass]
	public class ControladorCuentaTest
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
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorCuenta_Inicializa_Correctamente()
		{
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorCuenta_AhorrosDeEspacio_Retorna_ListaDeAhorros()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);

			List<AhorroDTO> ahorros = controladorTest.AhorrosDeEspacio(espacio.Id);
			Assert.AreEqual(2, ahorros.Count);
			Assert.AreEqual(ahorro.Nombre, ahorros[0].Nombre);
			Assert.AreEqual(ahorro.Monto, ahorros[0].Monto);
			Assert.AreEqual(ahorro.FechaCreacion, ahorros[0].FechaCreacion);
			Assert.AreEqual(ahorro2.Nombre, ahorros[1].Nombre);
			Assert.AreEqual(ahorro2.Monto, ahorros[1].Monto);
			Assert.AreEqual(ahorro2.FechaCreacion, ahorros[1].FechaCreacion);
		}

		[TestMethod]
		public void ControladorCuenta_CreditosDeEspacio_Retorna_ListaDeCreditos()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);

			List<CreditoDTO> creditos = controladorTest.CreditosDeEspacio(espacio.Id);
			Assert.AreEqual(1, creditos.Count);
			Assert.AreEqual(credito1.BancoEmisor, creditos[0].BancoEmisor);
			Assert.AreEqual(credito1.NumeroTarjeta, creditos[0].NumeroTarjeta);
			Assert.AreEqual(credito1.FechaCreacion, creditos[0].FechaCreacion);
			Assert.AreEqual(credito1.FechaCierre, creditos[0].FechaCierre);
			Assert.AreEqual(credito1.CreditoDisponible, creditos[0].CreditoDisponible);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarAhorro_Elimina_Ahorro_De_Espacio()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO = new AhorroDTO
			{
				Id = ahorro2.Id,
				Nombre = ahorro2.Nombre,
				Monto = ahorro2.Monto,
				FechaCreacion = ahorro2.FechaCreacion,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.EliminarAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual(2, espacio.Cuentas.Count);
			Assert.IsFalse(espacio.Cuentas.Contains(ahorro2));
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarAhorro_Retorna_Mensaje_Excepcion()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO = new AhorroDTO
			{
				Id = ahorro2.Id,
				Nombre = ahorro2.Nombre,
				Monto = ahorro2.Monto,
				FechaCreacion = ahorro2.FechaCreacion,
			};
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				Titulo = "Test",
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.EliminarAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.IsTrue(espacio.Cuentas.Contains(ahorro2));
			Assert.AreEqual("No se puede borrar una categoría que tiene transacciones asociadas", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarCredito_Elimina_Credito_De_Espacio()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.EliminarCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(2, espacio.Cuentas.Count);
			Assert.IsFalse(espacio.Cuentas.Contains(credito1));
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarCredito_Retorna_Mensaje_Excepcion()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			CreditoDTO creditoDTO = new CreditoDTO
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};

			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = credito1,
				Id = 1,
				Monto = 100,
				Titulo = "Test",
			};
			espacio.Transacciones.Add(transaccion);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.EliminarCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.IsTrue(espacio.Cuentas.Contains(credito1));
			Assert.AreEqual("No se puede borrar una categoría que tiene transacciones asociadas", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modifica_Ahorro_Exitosamente()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AddUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};

			Ahorro ahorro = new Ahorro
			{
				Id = 3,
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			AhorroDTO ahorroModificado = new AhorroDTO
			{
				Id = 3,
				Nombre = "AhorroModificado",
				Monto = 105550,
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.ModificarAhorro(espacio.Id, ahorroModificado);

			Assert.AreEqual(ahorroModificado.Nombre, ahorro.Nombre);
			Assert.AreEqual(ahorroModificado.Monto, ahorro.Monto);
			Assert.AreEqual("", mensaje);
		}


	}
}
