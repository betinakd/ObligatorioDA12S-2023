using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository;
using BussinesLogic;


namespace BussinesLogicTest
{
    [TestClass]
    public class EspacioLogicTest
    {
        private IRepository<Espacio> repository;
        private EspacioLogic espacioLogic;
        private Espacio espacio1;
        private Espacio espacio2;

        [TestInitialize]
        public void Setup()
        {
            repository = new EspacioMemoryRepository();
            espacioLogic = new EspacioLogic(repository);
            espacio1 = new Espacio()
            {
                Admin = new Usuario()
                {
                    Correo = "xx@yy.com",
                    Contrasena = "123456789A",
                }
            };

            espacio2 = new Espacio()
            { 
                Admin = new Usuario()
                {
                    Correo = "xxxx@yyyy.com",
                    Contrasena = "123456789B",
                }
            };
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
            Assert.IsTrue(repository.FindAll().Contains(espacio1));
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
            Assert.IsFalse(repository.FindAll().Contains(espacio1));
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
            Assert.AreEqual(espacio1, espacioLogic.FindEspacio(espacio1.Admin.Correo));
            Assert.AreEqual(espacio2, espacioLogic.FindEspacio(espacio2.Admin.Correo));
        }
    }
}
