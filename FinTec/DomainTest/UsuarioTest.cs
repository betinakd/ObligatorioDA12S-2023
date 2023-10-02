using Domain;

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

        [TestMethod]
        public void Usuario_Equals_Null()
        {
            Usuario unUsuario = new Usuario { 
                Contrasena = "1234567890",
                Correo = "usfhud@icsdc.comfwef",
            };
            Object objeto = null;
            var objeto2 = new Object();
            Assert.IsFalse(unUsuario.Equals(objeto));
            Assert.IsFalse(unUsuario.Equals(objeto2));
            Assert.IsTrue(unUsuario.Equals(unUsuario));
        }

        [TestMethod]
        public void Usuario_Equals_Diferentes()
        {
            Usuario user1 = new Usuario
            {
                Contrasena = "1234567890",
                Correo = "usfhud@icsdc.comfwef",
            };
            Usuario user2 = new Usuario
            {
                Contrasena = "1234567890",
                Correo = "12345@icsdc.comsds",
            };
            Assert.IsFalse(!user1.Equals(user2));
        }
    }
}
