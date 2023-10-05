using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Repository;


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
                    Correo = "",
                    Contrasena = "",
                }
            };

            espacio2 = new Espacio()
            { 
                Admin = new Usuario()
                {
                    Correo = "",
                    Contrasena = "",
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
    }
}
