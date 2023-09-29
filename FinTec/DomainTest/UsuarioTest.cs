using Domain;
using Domain.DomainExceptions;

namespace DomainTest
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

        [TestMethod]
        public void Contrasena_Maximo_Treinta()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Contrasena = "1234567890123456789012345678901";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Contrasena_Contiene_Mayuscula()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Contrasena = "contrasena";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Validar_Correo_Contiene_Arroba()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Correo = "usfhu@dicsdc.com";
            string correo = unUsuario.Correo;
            bool resultado = unUsuario.Validar_Correo(correo);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Validar_Correo_No_Coincide_PuntoCom()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Correo = "usfhud@icsdc.comfwef";
            string correo = unUsuario.Correo;            
            bool resultado = unUsuario.Validar_Correo(correo);
            Assert.IsFalse(resultado);
        }

        [ExpectedException(typeof(RequiredPropertyException))]
        [TestMethod]
        public void Nombre_Usuario_Vacio()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Nombre = "";            
        }

        [TestMethod]
        public void Obtener_Usuario()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Nombre = "Maxi";
            Assert.AreEqual("Maxi", unUsuario.Nombre);
        }
    }
}
