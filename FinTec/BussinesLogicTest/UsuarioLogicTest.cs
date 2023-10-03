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
    public class UsuarioLogicTest
    {
        [TestMethod]
        public void Nuevo_UsuarioLogic()
        {
            IRepository<Usuario> repository = new UsuarioMemoryRepository();
            UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
            Assert.IsNotNull(usuarioLogic);
        }
        
    }
}
