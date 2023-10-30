using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;
namespace RepositoryTest
{
    [TestClass]
    public class UsuarioMemoryTest
    {
		private Usuario _usuario1;
        private Usuario _usuario2;
		private UsuariosDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<UsuariosDbContext>()
                .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = EspaciosTest;" +
                " Integrated Security = True; Connect Timeout = 30; Encrypt = False").Options;

			_context = new UsuariosDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

			_usuario1 = new Usuario
			{
				Correo = "usuario1@yy.com",
				Contrasena = "123456789A",
				Nombre = "Usuario1",
				Apellido = "1",
				Direccion = "Dir",
			};
            _usuario2 = new Usuario
            {
                Correo = "usuario2@yy.com",
                Contrasena = "123456789B",
                Nombre = "Usuario2",
                Apellido = "2",
                Direccion = "Direccion",
            };
		}

		[TestMethod]
        public void Agregar_Usuario()
        {
            var repository = new UsuarioMemoryRepository(_context);
            var usuarioAgregado1 = repository.Add(_usuario1);
            var usuarioAgregado2 = repository.Add(_usuario2);
            Assert.IsNotNull(usuarioAgregado1);
            Assert.AreEqual(_usuario1, usuarioAgregado1);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual(_usuario2, usuarioAgregado2);   
        }
        /*
        [TestMethod]
        public void Actualizar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            usuario1.Contrasena = "1234567Yuuuuui";
            var usuarioAgregado2 = repository.Update(usuario1);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual("1234567Yuuuuui", usuarioAgregado2.Contrasena);
        }

        [TestMethod]
        public void Eliminar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            repository.Delete(usuario1.Correo);
            var usuarioAgregado2 = repository.Find(u => u.Correo == usuario1.Correo);
            Assert.IsNull(usuarioAgregado2);
        }

        [TestMethod]
        public void Buscar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Find(u => u.Correo == usuario1.Correo);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual(usuario1, usuarioAgregado2);
        }

        [TestMethod]
        public void Buscar_Todos_Usuarios()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var usuario2 = new Usuario
            {
                Correo = "Jose@xxxxx.com",
                Contrasena = "1234567Yuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Add(usuario2);
            var usuarios = repository.FindAll();
            Assert.IsNotNull(usuarios);
            Assert.AreEqual(2, usuarios.Count);
        } */
    }
}