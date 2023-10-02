using Domain;
using Repository;
namespace RepositoryTest
{
    [TestClass]
    public class UsuarioMemoryTest
    {
        [TestMethod]
        public void Agregar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456",
            };
            var usuario2 = new Usuario
            {
                Correo = "Juann@xxxx.com",
                Contrasena = "123456",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Add(usuario2);
            Assert.IsNotNull(usuarioAgregado1);
            Assert.AreEqual(usuario1, usuarioAgregado1);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual(usuario2, usuarioAgregado2);   
        }
    }
}