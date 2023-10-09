using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Espacio _espacio;
        private EspacioMemoryRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _cambios = new List<Cambio>();
            _objetivos = new List<Objetivo>();
            _transacciones = new List<Transaccion>();
            _categorias = new List<Categoria>();
            _cuentas = new List<Cuenta>();
            _usuariosInvitados = new List<Usuario>();
            _admin = new Usuario
            {
                Correo = "usuarioadmin@yy.com",
                Contrasena = "123456789A",
            };
            Cambio cambio1 = new Cambio
            {
                FechaDeCambio = DateTime.Now,
                Pesos = 100,
                Moneda = TipoCambiario.Dolar,
            };
            _cambios.Add(cambio1);

            Transaccion transaccion1 = new Transaccion
            {

            };
            _transacciones.Add(transaccion1);
            Categoria categoria1 = new Categoria
            {
                Nombre = "Comida",
            };
            _categorias.Add(categoria1);
            Cuenta cuenta1 = new Cuenta
            {
                Moneda = TipoCambiario.Dolar,
            };
            _cuentas.Add(cuenta1);
            Usuario usuarioInvitado1 = new Usuario
            {
                Correo = "usuario2@yy.com",
                Contrasena = "123456789B",
            };
            _usuariosInvitados.Add(usuarioInvitado1);
			Objetivo objetivo1 = new Objetivo
			{
				Categorias = _categorias,
				MontoMaximo = 1000,
				Titulo = "Objetivo1",
			};
			_objetivos.Add(objetivo1);
		}
    
        [TestMethod]
        public void Agregar_Espacio() 
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            var repository = new EspacioMemoryRepository();
            var espacioAgregado1 = repository.Add(espacio1);
            Assert.IsNotNull(espacioAgregado1);
            Assert.AreEqual(espacio1, espacioAgregado1);
        }

        [TestMethod]
        public void Buscar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            var repository = new EspacioMemoryRepository();
            var espacioAgregado1 = repository.Add(espacio1);
            var espacioAgregado2 = repository.Find(e => e.Admin == _admin);
            Assert.IsNotNull(espacioAgregado2);
            Assert.AreEqual(espacio1, espacioAgregado2);
        }

        [TestMethod]
        public void Buscar_Todos_Espacios()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            var espacio2 = new Espacio();
            espacio2.Admin = _admin;
            var repository = new EspacioMemoryRepository();
            var espacioAgregado1 = repository.Add(espacio1);
            var espacioAgregado2 = repository.Add(espacio2);
            var espacios = repository.FindAll();
            Assert.IsNotNull(espacios);
            Assert.AreEqual(2, espacios.Count);
        }

        [TestMethod]
        public void Actualizar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            var repository = new EspacioMemoryRepository();
            var espacioAgregado1 = repository.Add(espacio1);
            espacio1.Admin = new Usuario
            {
                Correo = "usuario2@yy.com",
                Contrasena = "123456789B",
            };
            var espacioAgregado2 = repository.Update(espacio1);
            Assert.IsNotNull(espacioAgregado2);
            Assert.AreEqual(espacio1, espacioAgregado2);
        }

        [TestMethod]
        public void Eliminar_Espacio()
        {
            var espacio1 = new Espacio();
            espacio1.Admin = _admin;
            var repository = new EspacioMemoryRepository();
            var espacioAgregado1 = repository.Add(espacio1);
            repository.Delete(espacio1.Admin.Correo);
            var espacioAgregado2 = repository.Find(e => e.Admin == _admin);
            Assert.IsNull(espacioAgregado2);
        }
    }
}
