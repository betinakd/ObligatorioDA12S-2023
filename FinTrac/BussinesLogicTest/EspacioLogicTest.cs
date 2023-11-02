using Excepcion;
using Domain;
using Repository;
using BussinesLogic;


namespace BussinesLogicTest
{
    [TestClass]
    public class EspacioLogicTest
    {
        private IRepository<Espacio> _repository;
        private EspacioLogic espacioLogic;
        private Espacio espacio1;
        private Espacio espacio2;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
        Usuario usuario1;

		[TestInitialize]
        public void Setup()
        {
			_context = _contextFactory.CreateDbContext();
			_repository = new EspacioMemoryRepository(_context);
            espacioLogic = new EspacioLogic(_repository);

            usuario1 = new Usuario()
            {
                Nombre = "Maxi",
                Apellido = "Gimenez",
                Direccion = "address",
                Correo = "xx@yy.com",
                Contrasena = "123456789A",
            };

            espacio1 = new Espacio()
            {
                Nombre = "Espacio1",
                Admin = usuario1  
            };

            espacio2 = new Espacio()
            { 
                Nombre = "Espacio2",
                Admin = new Usuario()
                {
                    Nombre = "Max",
                    Apellido = "Gim",
                    Direccion = "direccion",
                    Correo = "xxxx@yyyy.com",
                    Contrasena = "123456789B",
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _context = null;
			_repository = null;
			espacioLogic = null;
		}

        [TestMethod]
        public void Nuevo_EspacioLogic()
        {           
            Assert.IsNotNull(espacioLogic);
        }

        [TestMethod]
        public void Agregar_Espacio()
        {
            espacioLogic.AddEspacio(espacio1);
			Assert.IsTrue(_repository.FindAll().Contains(espacio1));
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicEspacioException))]
        public void Agregar_Espacio_Duplicado()
        {
            espacioLogic.AddEspacio(espacio1);
            espacioLogic.AddEspacio(espacio1);
        }

        [TestMethod]
        public void Eliminar_Espacio()
        {
            espacioLogic.AddEspacio(espacio1);
            espacioLogic.DeleteEspacio(espacio1);
            Assert.IsFalse(_repository.FindAll().Contains(espacio1));
        }

        [TestMethod]
        public void Buscar_Todos_Espacios()
        {
            espacioLogic.AddEspacio(espacio1);
            espacioLogic.AddEspacio(espacio2);
            Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio1));
            Assert.IsTrue(espacioLogic.FindAllEspacios().Contains(espacio2));
        }

        [TestMethod]
        public void Buscar_Espacio()
        {
            espacioLogic.AddEspacio(espacio1);
            espacioLogic.AddEspacio(espacio2);
            Espacio espacio = new Espacio();
            espacio.Nombre = "Espacio";
            espacio.Admin = new Usuario()
            {
                Nombre = "Maximiliano",
                Apellido = "Gimenezz",
                Direccion = "address2",
                Correo = "c@c.com",
                Contrasena = "123456789C",
            };
            espacioLogic.AddEspacio(espacio);
            Espacio resultado1 = espacioLogic.FindEspacio(espacio.Id);
			Assert.AreEqual(espacio.Nombre, resultado1.Nombre);
        }

        [TestMethod]
        public void Retorna_Lista_Espacios_Recibiendo_Correo_Valido() { 
            espacioLogic.AddEspacio(espacio1);
			espacioLogic.AddEspacio(espacio2);
			Espacio espacio = new Espacio();
			espacio.Nombre = "Espacio";
            espacio.Admin = usuario1;
            espacioLogic.AddEspacio(espacio);
			List<Espacio> espacios = espacioLogic.EspaciosByCorreo("xx@yy.com");
            Assert.IsTrue(espacios.Count == 2);
        }
    }
}
