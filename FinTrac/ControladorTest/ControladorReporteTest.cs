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
			Assert.IsTrue(reporte.Count != 1);
		}
	}
}
