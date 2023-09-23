using BussinesLogic;

namespace BussinesLogicTest
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Nuevo_Usuario()
        {
            var usuario = new Usuario();
            Assert.IsNotNull(usuario);
        }
    }
}
