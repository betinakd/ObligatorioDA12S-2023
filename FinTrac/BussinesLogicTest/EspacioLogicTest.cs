using Excepcion;
using Domain;
using Repository;
using BussinesLogic;


namespace BussinesLogicTest
{
	[TestClass]
	public class EspacioLogicTest
	{
		private IRepository<Espacio> _repository;
		private EspacioLogic espacioLogic;
		private Espacio espacio1;
		private Espacio espacio2;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		Usuario usuario1;

		[TestInitialize]
		public void Setup()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new EspacioMemoryRepository(_context);
			espacioLogic = new EspacioLogic(_repository);

			usuario1 = new Usuario()
			{
				Nombre = "Maxi",
				Apellido = "Gimenez",
				Direccion = "address",
				Correo = "xx@yy.com",
				Contrasena = "123456789A",
			};

			espacio1 = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = usuario1
			};

			espacio2 = new Espacio()
			{
				Nombre = "Espacio2",
				Admin = new Usuario()
				{
					Nombre = "Max",
					Apellido = "Gim",
					Direccion = "direccion",
					Correo = "xxxx@yyyy.com",
					Contrasena = "123456789B",
				}
			};
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
			espacioLogic.AddEspacio(espacio1);
			Assert.IsTrue(_repository.FindAll().Contains(espacio1));
		}

		[TestMethod]
		[ExpectedException(typeof(BusinessLogicEspacioException))]
		public void Agregar_Espacio_Duplicado()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio1);
		}

		[TestMethod]
		public void Eliminar_Espacio()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.DeleteEspacio(espacio1);
			Assert.IsFalse(_repository.FindAll().Contains(espacio1));
		}

		[TestMethod]
		public void Buscar_Todos_Espacios()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio1));
			Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio2));
		}

		[TestMethod]
		public void Buscar_Espacio()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			Espacio espacio = new Espacio();
			espacio.Nombre = "Espacio";
			espacio.Admin = new Usuario()
			{
				Nombre = "Maximiliano",
				Apellido = "Gimenezz",
				Direccion = "address2",
				Correo = "c@c.com",
				Contrasena = "123456789C",
			};
			espacioLogic.AddEspacio(espacio);
			Espacio resultado1 = espacioLogic.FindEspacio(espacio.Id);
			Assert.AreEqual(espacio.Nombre, resultado1.Nombre);
		}

		[TestMethod]
		public void Retorna_Lista_Espacios_Recibiendo_Correo_Valido()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			Espacio espacio = new Espacio();
			espacio.Nombre = "Espacio";
			espacio.Admin = usuario1;
			espacioLogic.AddEspacio(espacio);
			List<Espacio> espacios = espacioLogic.EspaciosByCorreo("xx@yy.com");
			Assert.IsTrue(espacios.Count == 2);
		}

		[TestMethod]
		public void Update_Recibe_Espacio_Modificado_Y_Actualiza_Correctamente()
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
			espacioLogic.AddEspacio(espacio1);
			espacio1.Nombre = "Espacio 2";
			espacioLogic.UpdateEspacio(espacio1);
			Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio1));
			Assert.AreEqual(espacio1.Nombre, espacioLogic.FindEspacio(espacio1.Id).Nombre);
		}

		[TestMethod]
		public void EspacioMayorId_No_Hay_Espacios_Null_Retorna_0()
		{
			int resultado = espacioLogic.EspacioMayorId();
			Assert.AreEqual(0, resultado);
		}

		[TestMethod]
		public void EspacioMayorId_Retorna_El_Id_Mayor_De_Espacio()
		{
			espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			int resultado = espacioLogic.EspacioMayorId();
			Assert.AreEqual(2, resultado);
		}

		[TestMethod]
		public void CrearEspacio_Crea_Espacio_Correctamente()
		{
			espacioLogic.CrearEspacio("Espacio1", usuario1);
			Assert.IsTrue(espacioLogic.FindAllEspacios().Count == 1);
			Assert.AreEqual("Espacio1", espacioLogic.FindAllEspacios()[0].Nombre);
		}

		[TestMethod]
		public void ModificarEspacioNombre_Modifica_El_Nombre_De_Espacio()
		{
			Espacio espacio = new Espacio
			{
				Id = 5,
				Nombre = "Test",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 5,
					Nombre = "test",
					Apellido = "test",
					Direccion = "address2",
					Correo = "test@gmail.com",
					Contrasena = "123456789Csss"
				}
			};
			string nombreAnterior = espacio.Nombre;

			espacioLogic.AddEspacio(espacio);
			espacioLogic.ModificarNombreEspacio(espacio.Id, "EspacioModificado");

			Espacio espacioModificado = espacioLogic.FindEspacio(espacio.Id);
			Assert.AreEqual("EspacioModificado", espacioModificado.Nombre);
			Assert.AreNotEqual(nombreAnterior, espacioModificado.Nombre);
		}

		[TestMethod]
		public void AgregarUsuarioAEspacio_Agrega_Usuario_Al_Espacio()
		{
			Espacio espacio = new Espacio
			{
				Id = 5,
				Nombre = "Test",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 5,
					Nombre = "test",
					Apellido = "test",
					Direccion = "address2",
					Correo = "test@gmail.com",
					Contrasena = "123456789Csss"
				}
			};
			Usuario usuario = new Usuario
			{
				IdEspacioPrincipal = 6,
				Nombre = "testing",
				Apellido = "teset",
				Direccion = "address2",
				Contrasena = "123456789Caaaa",
				Correo = "hola@gmail.com"
			};
			espacioLogic.AddEspacio(espacio);
			espacioLogic.AgregarUsuarioAEspacio(espacio.Id, usuario);
			bool resultado = espacioLogic.FindEspacio(espacio.Id).UsuariosInvitados.Contains(usuario);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		public void EliminarUsuarioDeEspacio_Elimina_Usuario_De_Espacio()
		{
			Espacio espacio = new Espacio
			{
				Id = 5,
				Nombre = "Test",
				Admin = new Usuario
				{
					IdEspacioPrincipal = 5,
					Nombre = "test",
					Apellido = "test",
					Direccion = "address2",
					Correo = "test@gmail.com",
					Contrasena = "123456789Csss"
				}
			};
			Usuario usuario = new Usuario
			{
				IdEspacioPrincipal = 6,
				Nombre = "testing",
				Apellido = "teset",
				Direccion = "address2",
				Contrasena = "123456789Caaaa",
				Correo = "hola@gmail.com"
			};
			espacio.UsuariosInvitados.Add(usuario);
			espacioLogic.AddEspacio(espacio);
			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).UsuariosInvitados.Contains(usuario));
			espacioLogic.EliminarUsuarioDeEspacio(espacio.Id, usuario);
			Assert.IsFalse(espacioLogic.FindEspacio(espacio.Id).UsuariosInvitados.Contains(usuario));
		}


		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
		public void EliminarUsuarioDeEspacio_Lanza_Excepcion_Usuario_Null()
		{
			Usuario admin = new Usuario
			{
				IdEspacioPrincipal = 5,
				Nombre = "test",
				Apellido = "test",
				Direccion = "address2",
				Correo = ""
			};
			Espacio espacio = new Espacio
			{
				Id = 5,
				Nombre = "Test",
				Admin = admin
			};
			Usuario usuario = new Usuario
			{
				IdEspacioPrincipal = 6,
				Nombre = "testing",
				Apellido = "teset",
				Direccion = "address2",
				Contrasena = "123456789Caaaa",
				Correo = ""
			};
			espacioLogic.AddEspacio(espacio);
			espacioLogic.EliminarUsuarioDeEspacio(espacio.Id, null);

		}

		[TestMethod]
		public void EspacioLogic_Elimina_Cuenta_De_Un_Espacio()
		{
			Usuario admin = new Usuario
			{
				IdEspacioPrincipal = 1,
				Nombre = "test",
				Apellido = "test",
				Direccion = "address2",
				Correo = "test@gmail.com",
				Contrasena = "123456789Csss"
			};
			Espacio espacio = new Espacio
			{
				Id = 1,
				Nombre = "Test",
				Admin = admin
			};
			Cuenta ahorro = new Ahorro()
			{
				Nombre = "AhorroTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.AgregarCuenta(ahorro);
			espacioLogic.AddEspacio(espacio);
			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).Cuentas.Contains(ahorro));
			espacioLogic.EliminarCuentaDeEspacio(espacio.Id, ahorro);
			Assert.IsFalse(espacioLogic.FindEspacio(espacio.Id).Cuentas.Contains(ahorro));
		}

		[TestMethod]
		public void EspacioLogic_Modifica_Cuenta_De_Un_Espacio()
		{
			Usuario admin = new Usuario
			{
				IdEspacioPrincipal = 1,
				Nombre = "test",
				Apellido = "test",
				Direccion = "address2",
				Correo = "test@gmail.com",
				Contrasena = "123456789Csss"
			};
			Espacio espacio = new Espacio
			{
				Id = 1,
				Nombre = "Test",
				Admin = admin
			};
			Cuenta ahorro = new Ahorro()
			{
				Id = 1,
				Nombre = "AhorroTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
			};
			Cuenta ahorroModificada = new Ahorro()
			{
				Id = 1,
				Nombre = "Modificada",
				Saldo = 100000,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
			};
			Cuenta credito = new Credito()
			{
				Id = 2,
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				NumeroTarjeta = "1534",
				FechaCierre = new DateTime(2026, 1, 1),
				Moneda = TipoCambiario.PesosUruguayos,
			};
			Cuenta creditoModificada = new Credito()
			{
				Id = 2,
				BancoEmisor = "ModificoTest",
				Saldo = 16600,
				FechaCreacion = DateTime.Now,
				NumeroTarjeta = "1234",
				FechaCierre = new DateTime(2025, 1, 1),
				Moneda = TipoCambiario.PesosUruguayos,
			};

			espacio.AgregarCuenta(ahorro);
			espacio.AgregarCuenta(credito);

			espacioLogic.AddEspacio(espacio);
			espacioLogic.ModificarCuentaDeEspacio(espacio.Id, ahorroModificada);
			espacioLogic.ModificarCuentaDeEspacio(espacio.Id, creditoModificada);
			Assert.IsTrue(ahorro.Equals(ahorroModificada));
			Assert.IsTrue(credito.Equals(creditoModificada));
		}

		[TestMethod]
		public void EspacioLogic_Crea_Cuenta_De_Un_Espacio()
		{
			Usuario admin = new Usuario
			{
				IdEspacioPrincipal = 1,
				Nombre = "test",
				Apellido = "test",
				Direccion = "address2",
				Correo = "test@gmail.com",
				Contrasena = "123456789Csss"
			};
			Espacio espacio = new Espacio
			{
				Id = 1,
				Nombre = "Test",
				Admin = admin
			};
			Cuenta ahorro = new Ahorro()
			{
				Id = 1,
				Nombre = "AhorroTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
			};
			Cuenta credito = new Credito()
			{
				Id = 2,
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				NumeroTarjeta = "1534",
				FechaCierre = new DateTime(2026, 1, 1),
				Moneda = TipoCambiario.Dolar,
			};
			Cuenta credito2 = new Credito()
			{
				Id = 3,
				BancoEmisor = "CreditoTest2",
				Saldo = 1000,
				FechaCreacion = DateTime.Now,
				NumeroTarjeta = "1544",
				FechaCierre = new DateTime(2026, 1, 1),
				Moneda = TipoCambiario.Euro,
			};
			espacioLogic.AddEspacio(espacio);
			espacioLogic.CrearCuenta(espacio.Id, credito);
			espacioLogic.CrearCuenta(espacio.Id, credito2);
			espacioLogic.CrearCuenta(espacio.Id, ahorro);
			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).Cuentas.Contains(ahorro));
			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).Cuentas.Contains(credito));
			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).Cuentas.Contains(credito2));
		}

		[TestMethod]
		public void EspacioLogic_Crea_Transaccion_De_Un_Espacio()
		{
			Usuario admin = new Usuario
			{
				IdEspacioPrincipal = 1,
				Nombre = "test",
				Apellido = "test",
				Direccion = "address2",
				Correo = "test@gmail.com",
				Contrasena = "123456789Csss"
			};

			Espacio espacio = new Espacio
			{
				Id = 1,
				Nombre = "Test",
				Admin = admin
			};

			Cuenta credito = new Credito()
			{
				Id = 2,
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				NumeroTarjeta = "1534",
				FechaCierre = new DateTime(2026, 1, 1),
				Moneda = TipoCambiario.PesosUruguayos,
			};

			Categoria cat = new Categoria()
			{
				Id = 1,
				Nombre = "CategoriaTest",
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				FechaCreacion = DateTime.Now,
			};

			espacio.Categorias.Add(cat);

			Transaccion transaccion = new Transaccion()
			{
				Id = 1,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Monto = 100,
				Titulo = "TransaccionTest",
				CuentaMonetaria = credito,
				CategoriaTransaccion = cat
			};
			espacio.Cuentas.Add(credito);
			espacioLogic.AddEspacio(espacio);
			espacioLogic.CrearTransaccion(espacio.Id, transaccion);

			Assert.IsTrue(espacioLogic.FindEspacio(espacio.Id).Transacciones.Contains(transaccion));
		}
	}
}
