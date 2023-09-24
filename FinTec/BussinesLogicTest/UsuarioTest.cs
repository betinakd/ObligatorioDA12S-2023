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

        [TestMethod]
        public void Contrasena_Minimo_Diez()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Contrasena = "1234567890";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsTrue(resultado);
        }

    }
}
