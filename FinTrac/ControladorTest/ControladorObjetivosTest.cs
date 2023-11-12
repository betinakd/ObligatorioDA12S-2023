using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using DTO;
using DTO.EnumsDTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorObjetivosTest
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
				Correo = "Juan@a.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			var usuario2 = new Usuario()
			{
				Correo = "Alberto@a.com",
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
		public void ControladorObjetivos_Tiene_EspacioLogic()
		{
			ControladorObjetivos controladorTest = new ControladorObjetivos(_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorObjetivos_ObjetivosDeEspacio()
		{
			ControladorObjetivos controladorTest = new ControladorObjetivos(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			List<ObjetivoDTO> categorias = controladorTest.ObjetivosDeEspacio(espacio.Id);
			Assert.AreEqual(0, categorias.Count);
		}

		[TestMethod]
		public void ControladorObjetivos_ObjetivosDeEspacio_TieneObjetivo()
		{
			ControladorObjetivos controladorTest = new ControladorObjetivos(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
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
			List<ObjetivoDTO> objetivos = controladorTest.ObjetivosDeEspacio(espacio.Id);
			Assert.AreEqual(1, objetivos.Count);
			Assert.AreEqual(1, objetivos[0].Id);
		}

		[TestMethod]
		public void ControladorObjetivos_CrearObjetivo()
		{
			ControladorObjetivos controladorTest = new ControladorObjetivos(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoria = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoriaDTO.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			ControladorCategorias controladorCategorias = new ControladorCategorias(_espacioLogic);
			controladorCategorias.CrearCategoria(1, categoria);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoriaDTO.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};

			List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>
			{
				categoriaDTO
			};

			ObjetivoDTO objetivoDTO = new ObjetivoDTO()
			{
				Id = 1,
				Titulo = "Objetivo 1",
				MontoMaximo = 1000,
				Categorias = categoriasDTO,
				Token = "Token"
			};

			string msjError = controladorTest.CrearObjetivo(1, objetivoDTO);
			Assert.AreEqual("", msjError);
		}
	}
}
