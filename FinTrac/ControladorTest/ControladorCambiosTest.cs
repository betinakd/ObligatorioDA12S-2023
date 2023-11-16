using LogicaNegocio;
using Dominio;
using Repositorio;
using Controlador;
using DTO;
using DTO.EnumsDTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorCambiosTest
	{
		private IRepositorio<Usuario> _repositorioUsuario;
		private UsuarioLogica _usuarioLogic;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		private IRepositorio<Espacio> _repositorioEspacio;
		private EspacioLogica _espacioLogic;
		private Espacio _espacio;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repositorioUsuario = new UsuarioMemoriaRepositorio(_context);
			_usuarioLogic = new UsuarioLogica(_repositorioUsuario);
			_repositorioEspacio = new EspacioMemoriaRepositorio(_context);
			_espacioLogic = new EspacioLogica(_repositorioEspacio);

			var usuario1 = new Usuario()
			{
				Correo = "Juan@a.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AgregarUsuario(usuario1);

			_espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.EncontrarUsuario("Juan@a.com")
			};
			_espacioLogic.AgregarEspacio(_espacio);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorCambios_Tiene_EspacioLogic()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorCambios_CambiosDeEspacio()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			List<CambioDTO> cambios = controladorTest.CambiosDeEspacio(1);
			Assert.AreEqual(0, cambios.Count);
		}

		[TestMethod]
		public void ControladorCambios_CambiosDeEspacio_TieneCambio()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			Cambio cambio = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			_espacio.AgregarCambio(cambio);
			_espacioLogic.UpdateEspacio(_espacio);
			List<CambioDTO> cambios = controladorTest.CambiosDeEspacio(1);
			Assert.AreEqual(1, cambios.Count);
			Assert.AreEqual(1, cambios[0].Id);
		}

		[TestMethod]
		public void ControladorCambios_CrearCambio()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			CambioDTO cambioDTO = new CambioDTO()
			{
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			string msjError = controladorTest.CrearCambio(1, cambioDTO);
			Assert.AreEqual("", msjError);
		}

		[TestMethod]
		public void ControladorCambios_CrearCambio_Error()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			CambioDTO cambioDTO = new CambioDTO()
			{
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			controladorTest.CrearCambio(1, cambioDTO);
			CambioDTO cambioDTO2 = new CambioDTO()
			{
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			controladorTest.CrearCambio(1, cambioDTO2);
			string msjError = controladorTest.CrearCambio(1, cambioDTO);
			Assert.AreEqual("Ya existe un cambio para la fecha.", msjError);
		}

		[TestMethod]
		public void ControladorCambios_ModificarCambio()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			CambioDTO cambioDTO = new CambioDTO()
			{
				Id = 1,
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			double valor = 200;
			controladorTest.CrearCambio(1, cambioDTO);
			cambioDTO.Pesos = valor;
			string msjError = controladorTest.ModificarCambio(1, cambioDTO);
			Assert.AreEqual("", msjError);
		}

		[TestMethod]
		public void ControladorCambios_ModificarCambio_Error()
		{
			ControladorCambios controladorTest = new ControladorCambios(_espacioLogic);
			CambioDTO cambioDTO = new CambioDTO()
			{
				Id = 1,
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = new DateTime(2021, 10, 10),
				Pesos = 100
			};
			controladorTest.CrearCambio(1, cambioDTO);
			double valor = 0;
			cambioDTO.Pesos = valor;
			string msjError = controladorTest.ModificarCambio(1, cambioDTO);
			Assert.AreEqual("El monto en pesos uruguayos debe ser mayor a 0", msjError);
		}
	}
}
