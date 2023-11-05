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
			UsuarioLogic _usuarioLogic = new UsuarioLogic(_repository);
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
	}
}