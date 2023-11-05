using BussinesLogic;
using Domain;
using Repository;
using Controlador;

namespace ControladorTest
{
	[TestClass]
	public class ControladorHomeTest
	{
		private UsuarioLogic _usuarioLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
			UsuariosDbContext _context = _contextFactory.CreateDbContext();
			IRepository<Usuario> _repository = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repository); // Asigna la variable de instancia
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

		[TestMethod]
		public void ControladorHome_Tiene_UsuarioLogic()
		{
			ControladorHome controladorTest = new ControladorHome();
			controladorTest.UsuarioLogic = _usuarioLogic;
			Assert.IsNotNull(controladorTest.UsuarioLogic);
			Assert.AreEqual(_usuarioLogic, controladorTest.UsuarioLogic);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Nombre()
		{
			ControladorHome controladorTest = new ControladorHome();
			controladorTest.Nombre = "Juan";
			Assert.IsNotNull(controladorTest.Nombre);
			Assert.AreEqual("Juan", controladorTest.Nombre);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Apellido()
		{
			ControladorHome controladorTest = new ControladorHome();
			controladorTest.Apellido = "Perez";
			Assert.IsNotNull(controladorTest.Apellido);
			Assert.AreEqual("Perez", controladorTest.Apellido);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Direccion()
		{
			ControladorHome controladorTest = new ControladorHome();
			controladorTest.Direccion = "street 56 av rety";
			Assert.IsNotNull(controladorTest.Direccion);
			Assert.AreEqual("street 56 av rety", controladorTest.Direccion);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Correo()
		{
			ControladorHome controladorTest = new ControladorHome();
			controladorTest.Correo = "test@gmail.com";
		}

	}
}