using Controlador;
using DTO;
using Domain;
using EspacioReporte;
using DTO.EnumsDTO;
using BussinesLogic;
using Repository;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

namespace ControladorTest
{

	

	[TestClass]
	public class ControladorReporteTest
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
		public void ControladorReporte_Se_Crea()
		{
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			Assert.IsNotNull(controladorReporte);
		}


		[TestMethod]
		public void ReporteObjetivosGasto_Existe()
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
			espacio.AgregarCuenta(ahorro);
			List<Categoria> categorias = new List<Categoria>()
			{
				new Categoria()
				{
					Id = 1,
					Nombre = "Categoria1",
					Tipo = TipoCategoria.Costo,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now
				}
			};
			Objetivo objetivo = new Objetivo()
			{
				Id = 1,
				Titulo = "Objetivo 1",
				MontoMaximo = 1000,
				Categorias = categorias,
				Token = null
			};
			espacio.AgregarObjetivo(objetivo);
			Transaccion transaccion = new Transaccion()
			{
				Id = 1,
				Moneda = ahorro.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categorias.First(),
				CuentaMonetaria = ahorro,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion);
			_espacioLogic.AddEspacio(espacio);
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<ObjetivoGastoDTO> reporte = controladorReporte.ReporteObjetivosGastos(1);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Ahorro_Pesos()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.AgregarCuenta(ahorro1);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = ahorro1.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = ahorro1,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo=true,
				FechaCreacion=DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				FechaCreacion = ahorro1.FechaCreacion,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Monto = ahorro1.Monto,
				Nombre = ahorro1.Nombre,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), ahorroDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Ahorro_Dolares()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar
			};
			espacio.AgregarCuenta(ahorro1);
			Cambio cambioDolar = new Cambio()
			{
				FechaDeCambio = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Pesos = 30,
			};
			espacio.AgregarCambio(cambioDolar);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = ahorro1.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = ahorro1,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				FechaCreacion = ahorro1.FechaCreacion,
				Moneda = TipoCambiarioDTO.Dolar,
				Monto = ahorro1.Monto,
				Nombre = ahorro1.Nombre,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), ahorroDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Ahorro_Euros()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro
			};
			espacio.AgregarCuenta(ahorro1);
			Cambio cambioEuro = new Cambio()
			{
				FechaDeCambio = DateTime.Today,
				Moneda = TipoCambiario.Euro,
				Pesos = 30,
			};
			espacio.AgregarCambio(cambioEuro);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = ahorro1.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = ahorro1,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				FechaCreacion = ahorro1.FechaCreacion,
				Moneda = TipoCambiarioDTO.Euro,
				Monto = ahorro1.Monto,
				Nombre = ahorro1.Nombre,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), ahorroDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Credito()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), creditoDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Credito_Euros()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.Euro,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Cambio cambio = new Cambio()
			{
				Moneda = TipoCambiario.Euro,
				Pesos = 40,
				FechaDeCambio = DateTime.Today,
			};
			espacio.AgregarCambio(cambio);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.Euro,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), creditoDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteListadoGastos_Crea_Elemento_Cuenta_Credito_Dolares()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.Dolar,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Cambio cambio = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
				FechaDeCambio = DateTime.Today,
			};
			espacio.AgregarCambio(cambio);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.Dolar,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteDeGastos(1, categoriaDTO, DateTime.Today.AddMonths(-2), DateTime.Today.AddMonths(2), creditoDTO);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Da_1_Elemento_Dolar()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.Dolar,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Cambio cambioDolar = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 33,
				FechaDeCambio = DateTime.Today,
			};
			espacio.AgregarCambio(cambioDolar);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.Dolar,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteGastosTarjeta(1, creditoDTO.NumeroTarjeta);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Da_1_Elemento_Euro()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.Euro,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Cambio cambioEuro = new Cambio()
			{
				Moneda = TipoCambiario.Euro,
				Pesos = 33,
				FechaDeCambio = DateTime.Today,
			};
			espacio.AgregarCambio(cambioEuro);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.Euro,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteGastosTarjeta(1, creditoDTO.NumeroTarjeta);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Da_1_Elemento_Pesos()
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
			Credito credito = new Credito
			{
				BancoEmisor = "santander",
				CreditoDisponible = 1000,
				FechaCierre = DateTime.Today.AddMonths(2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "5555",
			};
			espacio.AgregarCuenta(credito);
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			Transaccion transaccion1 = new Transaccion()
			{
				Id = 1,
				Moneda = credito.Moneda,
				Monto = 1000,
				CategoriaTransaccion = categoria,
				CuentaMonetaria = credito,
				Titulo = "hola",
				FechaTransaccion = DateTime.Today,
			};
			espacio.AgregarTransaccion(transaccion1);
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = categoria.Id,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoriaDTO.Costo,
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				BancoEmisor = credito.BancoEmisor,
				FechaCierre = credito.FechaCierre,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				NumeroTarjeta = credito.NumeroTarjeta,
				CreditoDisponible = credito.CreditoDisponible,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			List<TransaccionDTO> reporte = controladorReporte.ReporteGastosTarjeta(1, creditoDTO.NumeroTarjeta);
			Assert.IsTrue(reporte.Count == 1);
		}

		[TestMethod]
		public void ReporteBalanceCuentas_Genera_Valor_Bien()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 200,
				FechaCreacion = DateTime.Today,
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.AgregarCuenta(ahorro1);
			Categoria categoria = new Categoria()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "categoria Gasto prueba",
				Tipo = TipoCategoria.Ingreso,
			};
			espacio.AgregarCategoria(categoria);
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = ahorro1,
				FechaTransaccion = DateTime.Today,
				Moneda = ahorro1.Moneda,
				Monto = 100,
				Titulo = "Transaccion de prueba",
				CategoriaTransaccion = categoria,
			};
			espacio.AgregarTransaccion(transaccion);
			_espacioLogic.AddEspacio(espacio);
			AhorroDTO ahorroEnDTO = new AhorroDTO()
			{
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Monto = ahorro1.Monto,
				Nombre = ahorro1.Nombre,
				FechaCreacion = ahorro1.FechaCreacion,
			};
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			double valorTotal = controladorReporte.BalanceDeCuentas(1, ahorroEnDTO);
			Assert.IsTrue(valorTotal == ahorro1.Monto);
		}

		[TestMethod]
		public void ReporteIngresosEgresos_Genera_Lista_Bien()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 200,
				FechaCreacion = DateTime.Today,
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.AgregarCuenta(ahorro1);
			Categoria categoria = new Categoria()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "categoria Gasto prueba",
				Tipo = TipoCategoria.Ingreso,
			};
			espacio.AgregarCategoria(categoria);
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = ahorro1,
				FechaTransaccion = DateTime.Today,
				Moneda = ahorro1.Moneda,
				Monto = 100,
				Titulo = "Transaccion de prueba",
				CategoriaTransaccion = categoria,
			};
			espacio.AgregarTransaccion(transaccion);
			_espacioLogic.AddEspacio(espacio);
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			int mes = DateTime.Today.Month;
			List<IngresoEgresoDTO> reporte = controladorReporte.ReporteIngresosEgresos(1, mes);
			Assert.IsTrue(reporte.Count == 30);
		}

		[TestMethod]
		public void ReporteIngresosEgresos_Genera_Valor_Transaccion_Bien()
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
			Ahorro ahorro1 = new Ahorro
			{
				Nombre = "AhorroTest1",
				Monto = 200,
				FechaCreacion = DateTime.Today,
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.AgregarCuenta(ahorro1);
			Categoria categoria = new Categoria()
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "categoria Gasto prueba",
				Tipo = TipoCategoria.Ingreso,
			};
			espacio.AgregarCategoria(categoria);
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = ahorro1,
				FechaTransaccion = DateTime.Today,
				Moneda = ahorro1.Moneda,
				Monto = 100,
				Titulo = "Transaccion de prueba",
				CategoriaTransaccion = categoria,
			};
			espacio.AgregarTransaccion(transaccion);
			_espacioLogic.AddEspacio(espacio);
			ControladorReporte controladorReporte = new ControladorReporte(_espacioLogic);
			int mes = DateTime.Today.Month;
			DateTime fechaHoy = DateTime.Today;
			List<IngresoEgresoDTO> reporte = controladorReporte.ReporteIngresosEgresos(1, mes);
			Assert.IsTrue(reporte.ToArray()[fechaHoy.Day - 1].Ingresos == transaccion.Monto);
		}
	}
}
