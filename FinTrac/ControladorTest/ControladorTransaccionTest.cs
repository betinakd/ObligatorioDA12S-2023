using BussinesLogic;
using Domain;
using Repository;
using DTO;
using Controlador;
using DTO.EnumsDTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorTransaccionTest
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
		public void TransaccionesDatosTest()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Id = 1,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 2,
			};
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 3,
			};
			espacio.Cuentas.Add(ahorro3);
			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test",
					Id = 1,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Costo,
				},
			};
			Transaccion transaccion2 = new TransaccionIngreso()
			{
				CuentaMonetaria = ahorro,
				Id = 2,
				Monto = 1000,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Titulo = "Test2",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test2",
					Id = 2,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Ingreso,
				},
			};
			Transaccion transaccion3 = new TransaccionIngreso()
			{
				CuentaMonetaria = ahorro3,
				Id = 3,
				Monto = 10300,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Test3",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test3",
					Id = 3,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Ingreso,
				},
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			espacio.Transacciones.Add(transaccion2);
			espacio.Transacciones.Add(transaccion3);
			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);

			List<TransaccionDTO> transacciones = controladorTransaccion.TransaccionesDatos(1);

			Assert.AreEqual(3, transacciones.Count);
		}

		[TestMethod]
		public void ControladorTransacciones_Tiene_DatosCuenta()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Id = 1,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 2,
			};
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 3,
			};
			espacio.Cuentas.Add(ahorro3);
			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test",
					Id = 1,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Costo,
				},
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);

			List<string> transacciones = controladorTransaccion.DatosCuentasEspacio(1);

			Assert.AreEqual(3, transacciones.Count);
		}

		[TestMethod]
		public void ControladorTeansaccion_CreaTransaccion_De_Ingreso()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionIngreso(1, transaccion);

			Espacio resultado = _espacioLogic.FindEspacio(1);
			List<Transaccion> resultadoTransacciones = resultado.Transacciones;
			Assert.AreEqual(1, resultadoTransacciones.Count);
			Assert.AreEqual(100, resultadoTransacciones[0].Monto);
			Assert.AreEqual(TipoCambiario.PesosUruguayos, resultadoTransacciones[0].Moneda);
			Assert.AreEqual("Test", resultadoTransacciones[0].Titulo);
			Assert.AreEqual(categoria.Nombre, resultadoTransacciones[0].CategoriaTransaccion.Nombre);
			Assert.AreEqual(ahorro, resultadoTransacciones[0].CuentaMonetaria);
			Assert.AreEqual("Ingreso", resultadoTransacciones[0].Tipo());
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorTeansaccion_Lanza_Mensaje_Excepcion_Al_CreaTransaccion_De_Ingreso()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionIngreso(1, transaccion);

			Assert.AreEqual("No hay cotización cambiaria de Dolar para la fecha de hoy", mensaje);
		}

		[TestMethod]
		public void ControladorTeansaccion_CreaTransaccion_De_Costo()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionCosto(1, transaccion);

			Espacio resultado = _espacioLogic.FindEspacio(1);
			List<Transaccion> resultadoTransacciones = resultado.Transacciones;
			Assert.AreEqual(1, resultadoTransacciones.Count);
			Assert.AreEqual(100, resultadoTransacciones[0].Monto);
			Assert.AreEqual(TipoCambiario.PesosUruguayos, resultadoTransacciones[0].Moneda);
			Assert.AreEqual("Test", resultadoTransacciones[0].Titulo);
			Assert.AreEqual(categoria.Nombre, resultadoTransacciones[0].CategoriaTransaccion.Nombre);
			Assert.AreEqual(ahorro, resultadoTransacciones[0].CuentaMonetaria);
			Assert.AreEqual("Costo", resultadoTransacciones[0].Tipo());
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorTeansaccion_Lanza_Mensaje_Excepcion_Al_CreaTransaccion_De_Costo()
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
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AddEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionCosto(1, transaccion);

			Assert.AreEqual("No hay cotización cambiaria de Dolar para la fecha de hoy", mensaje);
		}

	}
}
