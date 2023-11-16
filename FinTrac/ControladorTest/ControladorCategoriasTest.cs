using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using DTO.EnumsDTO;
using DTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorCategoriasTest
	{
		private IRepository<Usuario> _repositorioUsuario;
		private UsuarioLogic _usuarioLogic;
		private FintracDbContext _context;
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
		public void ControladorCategorias_Tiene_EspacioLogic()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorCategorias_CategoriasDeEspacio()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			List<CategoriaDTO> categorias = controladorTest.CategoriasDeEspacio(1);
			Assert.AreEqual(0, categorias.Count);
		}

		[TestMethod]
		public void ControladorCategorias_CategoriasDeEspacio_TieneCategoria()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			Categoria categoria = new Categoria()
			{
				Id = 1,
				Nombre = "Categoria1",
				Tipo = TipoCategoria.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			espacio.AgregarCategoria(categoria);
			Categoria categoria2 = new Categoria()
			{
				Id = 2,
				Nombre = "Categoria2",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			espacio.AgregarCategoria(categoria2);
			_espacioLogic.AddEspacio(espacio);
			List<CategoriaDTO> categorias = controladorTest.CategoriasDeEspacio(1);
			Assert.AreEqual(2, categorias.Count);
			Assert.AreEqual(1, categorias[0].Id);
		}

		[TestMethod]
		public void ControladorCategorias_CrearCategoria()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			string msjError = controladorTest.CrearCategoria(1, categoriaDTO);
			Assert.AreEqual("", msjError);
		}

		[TestMethod]
		public void ControladorCategorias_CrearCategoria_Error()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoria1DTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			CategoriaDTO categoria2DTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			string msjError = controladorTest.CrearCategoria(1, categoria1DTO);
			msjError = controladorTest.CrearCategoria(1, categoria2DTO);
			Assert.AreEqual(msjError, "No se pueden agregar dos categorías con el mismo nombre.");
		}

		[TestMethod]
		public void ControladorCategorias_ModificarNombreCategoria()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			controladorTest.CrearCategoria(1, categoriaDTO);
			CategoriaDTO categoriaDTO2 = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria2",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			categoriaDTO2.Nombre = "Categoria3";
			string msjError = controladorTest.ModificarNombreCategoria(1, categoriaDTO2);
			Assert.AreEqual("", msjError);
		}

		[TestMethod]
		public void ControladorCategorias_ModificarNombreCategoria_Error()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			controladorTest.CrearCategoria(1, categoriaDTO);
			CategoriaDTO categoriaDTO2 = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria2",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			categoriaDTO2.Nombre = "Categoria1";
			string msjError = controladorTest.ModificarNombreCategoria(1, categoriaDTO2);
			Assert.AreEqual("Ya existe una categoría con ese nombre", msjError);
		}

		[TestMethod]
		public void ControladorCategorias_EliminarCategoria()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			controladorTest.CrearCategoria(1, categoriaDTO);
			CategoriaDTO categoriaDTO2 = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria2",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			string msjError = controladorTest.EliminarCategoria(1, categoriaDTO2);
			Assert.AreEqual("", msjError);
		}

		[TestMethod]
		public void ControladorCategorias_ModificarEstadoCategoria()
		{
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			Espacio espacio = new Espacio()
			{
				Nombre = "Espacio1",
				Admin = _usuarioLogic.FindUsuario("Juan@a.com")
			};
			_espacioLogic.AddEspacio(espacio);
			CategoriaDTO categoriaDTO = new CategoriaDTO()
			{
				Nombre = "Categoria1",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			controladorTest.CrearCategoria(1, categoriaDTO);
			CategoriaDTO categoriaDTO2 = new CategoriaDTO()
			{
				Id = 1,
				Nombre = "Categoria2",
				Tipo = DTO.EnumsDTO.TipoCategoriaDTO.Ingreso,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			categoriaDTO2.EstadoActivo = false;
			controladorTest.ModificarEstadoCategoria(1, categoriaDTO2);
			Assert.AreEqual(espacio.Categorias[0].EstadoActivo, false);
		}

		[TestMethod]
		public void Retorna_Mensaje_Excepcoion_Al_Intentar_Borrar_Categoria_Objetivo()
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
			Categoria categoria = new Categoria
			{
				Nombre = "Categoria",
				Tipo = TipoCategoria.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			List<Categoria> categorias = new List<Categoria>();		
			categorias.Add(categoria);
			Objetivo objetivo = new Objetivo
			{
				MontoMaximo = 1000,
				Titulo = "Objetivo",
				Categorias = categorias,
			};
			espacio.Categorias.Add(categoria);
			espacio.Objetivos.Add(objetivo);
			CategoriaDTO catDTO = new CategoriaDTO
			{
				Id = 1,
				Nombre = "Categoria",
				Tipo = TipoCategoriaDTO.Costo,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now
			};
			_espacioLogic.AddEspacio(espacio);
			ControladorCategorias controladorTest = new ControladorCategorias(_espacioLogic);
			string mensaje = controladorTest.EliminarCategoria(1, catDTO);
			Assert.AreEqual("No se puede borrar una categoría que asociada a algún objetivo.", mensaje);
		}
	}
}
