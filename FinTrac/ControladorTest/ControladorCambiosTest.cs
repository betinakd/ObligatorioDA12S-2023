using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using DTO;
using DTO.EnumsDTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorCambiosTest
	{
		private IRepository<Usuario> _repositorioUsuario;
		private UsuarioLogic _usuarioLogic;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		private IRepository<Espacio> _repositorioEspacio;
		private EspacioLogic _espacioLogic;
		private Espacio _espacio;

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
				Correo = "Juan@a.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);

			_espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(_espacio);
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
	}
}
