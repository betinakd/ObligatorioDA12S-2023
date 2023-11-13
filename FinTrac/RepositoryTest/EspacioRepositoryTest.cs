using Domain;
using Repository;

namespace RepositoryTest
{
    [TestClass]
    public class EspacioRepositoryTest
    {
        private List<Cambio> _cambios;
        private List<Objetivo> _objetivos;
        private List<Transaccion> _transacciones;
        private List<Categoria> _categorias;
        private List<Cuenta> _cuentas;
        private List<Usuario> _usuariosInvitados;
        private Usuario _admin;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
        public void TestInitialize()
        {
			_context = _contextFactory.CreateDbContext();
			
            _admin = new Usuario
            {
                Correo = "usuarioadmin@yy.com",
                Contrasena = "123456789A",
                Nombre = "Usuario",
                Apellido = "Admin",
                Direccion = "Dir",
            };
            
            _context.Usuarios.Add(_admin);
            _context.SaveChanges();

		}

        [TestCleanup]
        public void TestCleanup()
        {
			_context.Database.EnsureDeleted();
		}

        [TestMethod]
        public void Agregar_Espacio() 
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
			espacio1.Nombre = "Espacio1";
			var repository = new EspacioMemoryRepository(_context);
            var espacioAgregado1 = repository.Add(espacio1);
            var espacioInDb = _context.Espacios.First();
            Assert.AreEqual(espacio1, espacioInDb);
            Assert.IsNotNull(espacioAgregado1);
            Assert.AreEqual(espacio1, espacioAgregado1);
        }
        
        [TestMethod]
        public void Buscar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
			espacio1.Nombre = "Espacio1";
			var repository = new EspacioMemoryRepository(_context);
            var espacioAgregado1 = repository.Add(espacio1);
            var espacioAgregado2 = repository.Find(e => e.Admin == _admin);
			var espacioInDb = _context.Espacios.First();
            Assert.AreEqual(espacio1, espacioInDb);
			Assert.IsNotNull(espacioAgregado2);
            Assert.AreEqual(espacio1, espacioAgregado2);
        }

        [TestMethod]
        public void Buscar_Todos_Espacios()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            espacio1.Nombre = "Espacio1";
            var espacio2 = new Espacio();
            espacio2.Admin = _admin;
            espacio2.Nombre = "Espacio2";
            var repository = new EspacioMemoryRepository(_context);
            repository.Add(espacio1);
            repository.Add(espacio2);
            var espacios = repository.FindAll();
			var espacioInDb1 = _context.Espacios.First();
            var espacioInDb2 = _context.Espacios.Last();
			Assert.IsNotNull(espacio1);
			Assert.AreEqual(espacio1, espacioInDb1);
			Assert.IsNotNull(espacio2);
			Assert.AreEqual(espacio2, espacioInDb2);
			Assert.AreEqual(espacio1, espacioInDb1);
            Assert.IsNotNull(espacio2);
            Assert.AreEqual(espacio2, espacioInDb2);
			Assert.IsNotNull(espacios);
            Assert.AreEqual(2, espacios.Count);
        }

        [TestMethod]
        public void Actualizar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            espacio1.Nombre = "Espacio1";
            var repository = new EspacioMemoryRepository(_context);        
            repository.Add(espacio1);
            espacio1.Nombre = "Espacio2";
            var espacioAgregado2 = repository.Update(espacio1);
            var espacioInDb = _context.Espacios.First();
            Assert.AreEqual(espacio1.Nombre, espacioInDb.Nombre);
            Assert.IsNotNull(espacioAgregado2);
            Assert.AreEqual("Espacio2", espacioAgregado2.Nombre);
        }

        [TestMethod]
        public void Eliminar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            espacio1.Nombre = "Espacio1";
            var repository = new EspacioMemoryRepository(_context);
            var espacioAgregado1 = repository.Add(espacio1);
            repository.Delete(espacioAgregado1.Admin.Correo);
            var espacioAgregado2 = repository.Find(e => e.Admin == _admin);
            var espacioInDb = _context.Espacios.FirstOrDefault(e => e.Admin == _admin);
            Assert.IsNull(espacioInDb);
            Assert.IsNull(espacioAgregado2);
        } 
    }
}
