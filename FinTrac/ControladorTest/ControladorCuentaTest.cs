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
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Monto = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar
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
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Monto = 1050,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro
			};
			espacio.Cuentas.Add(ahorro3);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);

			List<AhorroDTO> ahorros = controladorTest.AhorrosDeEspacio(espacio.Id);
			Assert.AreEqual(3, ahorros.Count);
			Assert.AreEqual(ahorro.Nombre, ahorros[0].Nombre);
			Assert.AreEqual(ahorro.Monto, ahorros[0].Monto);
			Assert.AreEqual(ahorro.FechaCreacion, ahorros[0].FechaCreacion);
			Assert.AreEqual(ahorro2.Nombre, ahorros[1].Nombre);
			Assert.AreEqual(ahorro2.Monto, ahorros[1].Monto);
			Assert.AreEqual(ahorro2.FechaCreacion, ahorros[1].FechaCreacion);
			Assert.AreEqual(ahorro3.Nombre, ahorros[2].Nombre);
			Assert.AreEqual(ahorro3.Monto, ahorros[2].Monto);
			Assert.AreEqual(ahorro3.FechaCreacion, ahorros[2].FechaCreacion);
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
				Moneda = TipoCambiario.Dolar
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.Cuentas.Add(credito1);
			Credito credito2 = new Credito()
			{
				NumeroTarjeta = "1255",
				BancoEmisor = "Credito2Test",
				CreditoDisponible = 1070,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
				Moneda = TipoCambiario.Euro
			};
			espacio.Cuentas.Add(credito2);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);

			List<CreditoDTO> creditos = controladorTest.CreditosDeEspacio(espacio.Id);
			Assert.AreEqual(2, creditos.Count);
			Assert.AreEqual(credito1.BancoEmisor, creditos[0].BancoEmisor);
			Assert.AreEqual(credito1.NumeroTarjeta, creditos[0].NumeroTarjeta);
			Assert.AreEqual(credito1.FechaCreacion, creditos[0].FechaCreacion);
			Assert.AreEqual(credito1.FechaCierre, creditos[0].FechaCierre);
			Assert.AreEqual(credito1.CreditoDisponible, creditos[0].CreditoDisponible);
			Assert.AreEqual(credito2.BancoEmisor, creditos[1].BancoEmisor);
			Assert.AreEqual(credito2.NumeroTarjeta, creditos[1].NumeroTarjeta);
			Assert.AreEqual(credito2.FechaCreacion, creditos[1].FechaCreacion);
			Assert.AreEqual(credito2.FechaCierre, creditos[1].FechaCierre);
			Assert.AreEqual(credito2.CreditoDisponible, creditos[1].CreditoDisponible);
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
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Modifica_Ahorro_Mensaje_Excepcion()
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
				Nombre = "AhorroTest1",
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
			Assert.AreEqual("No se puede Modificar, hay cuentas ya registradas con ese nombre", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modifica_Credito_Exitosamente()
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
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 1,
				NumeroTarjeta = "1434",
				BancoEmisor = "ModificadoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.ModificarCredito(espacio.Id, creditoDTO);

			Assert.AreEqual(creditoDTO.NumeroTarjeta, credito.NumeroTarjeta);
			Assert.AreEqual(creditoDTO.BancoEmisor, credito.BancoEmisor);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Modifica_Credito_Mensaje_Excepcion()
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
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				CreditoDisponible = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
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
			string mensaje = controladorTest.ModificarCredito(espacio.Id, creditoDTO);

			Assert.AreEqual(creditoDTO.NumeroTarjeta, credito.NumeroTarjeta);
			Assert.AreEqual(creditoDTO.BancoEmisor, credito.BancoEmisor);
			Assert.AreEqual("No se puede Modificar, hay cuentas ya registradas con ese nombre", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Crea_CuentaAhorro_Con_Exito()
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
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest1",
				Monto = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Dolar,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO2 = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest2",
				Monto = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Euro,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO3 = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest3",
				Monto = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.PesosUruguayos,
				FechaCreacion = DateTime.Now,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.CrearAhorro(espacio.Id, ahorroDTO);
			string mensaje2 = controladorTest.CrearAhorro(espacio.Id, ahorroDTO2);
			string mensaje3 = controladorTest.CrearAhorro(espacio.Id, ahorroDTO3);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.AreEqual("", mensaje);
			Assert.AreEqual("", mensaje2);
			Assert.AreEqual("", mensaje3);
		}

		[TestMethod]
		public void ControladorCuenta_LNo_Crea_CuentaAhorro_Mensaje_Excepcion()
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
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				Id = 3,
				Nombre = "",
				Monto = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Dolar,
				FechaCreacion = DateTime.Now,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.CrearAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual("El nombre de la cuenta no puede ser vacío", mensaje);
		}


		[TestMethod]
		public void ControladorCuenta_Crea_CuentaCredito_Con_Exito()
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
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "BancoTest",
				NumeroTarjeta = "1234",
				CreditoDisponible = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.CrearCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(1, espacio.Cuentas.Count);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Crea_CuentaCredito_Mensaje_Excepcion()
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
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "",
				NumeroTarjeta = "1234",
				CreditoDisponible = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.CrearCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(0, espacio.Cuentas.Count);
			Assert.AreEqual("El banco emisor no puede ser vacío", mensaje);
		}

	}
}
