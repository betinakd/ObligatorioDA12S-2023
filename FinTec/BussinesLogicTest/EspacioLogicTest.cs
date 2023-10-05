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
            IRepository<Espacio> repository = new EspacioMemoryRepository();
            EspacioLogic espacioLogic = new EspacioLogic(repository);
            Assert.IsNotNull(espacioLogic);
        }

        [TestMethod]
        public void Agregar_Espacio()
        {
            IRepository<Espacio> repository = new EspacioMemoryRepository();
            EspacioLogic espacioLogic = new EspacioLogic(repository);
            Espacio espacio = new Espacio();
            espacio.Admin = new Usuario()
            {
                Correo = "xx@yy.com",
                Contrasena = "123456789A",
            };
            espacioLogic.AddEspacio(espacio);
            Assert.IsTrue(repository.FindAll().Contains(espacio));
        }

        [TestMethod]
        public void Eliminar_Espacio()
        {
            IRepository<Espacio> repository = new EspacioMemoryRepository();
            EspacioLogic espacioLogic = new EspacioLogic(repository);
            Espacio espacio = new Espacio();
            espacio.Admin = new Usuario()
            {
                Correo = "xx@yy.com",
                Contrasena = "123456789A",
            };
            espacioLogic.AddEspacio(espacio);
            espacioLogic.DeleteEspacio(espacio);
            Assert.IsFalse(repository.FindAll().Contains(espacio));
        }

        [TestMethod]
        public void Buscar_Todos_Espacios()
        {
            IRepository<Espacio> repository = new EspacioMemoryRepository();
            EspacioLogic espacioLogic = new EspacioLogic(repository);
            Espacio espacio = new Espacio();
            espacio.Admin = new Usuario()
            {
                Correo = "xx@yy.com",
                Contrasena = "123456789A",

            };
            Espacio espacio2 = new Espacio();
            espacio2.Admin = new Usuario()
            {
                Correo = "xxxx@yyyy.com",
                Contrasena = "123456789B",

            };  
            espacioLogic.AddEspacio(espacio);
            espacioLogic.AddEspacio(espacio2);
            Assert.IsTrue(repository.FindAll().Contains(espacio));
            Assert.IsTrue(repository.FindAll().Contains(espacio2));
        }
    }
}
