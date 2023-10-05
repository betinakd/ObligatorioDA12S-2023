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
                Contrasena = "123456Yuuuu",
            };
            var usuario2 = new Usuario
            {
                Correo = "Juann2@xxxx.com",
                Contrasena = "123456Yuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Add(usuario2);
            Assert.IsNotNull(usuarioAgregado1);
            Assert.AreEqual(usuario1, usuarioAgregado1);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual(usuario2, usuarioAgregado2);   
        }

        [TestMethod]
        public void Actualizar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            usuario1.Contrasena = "1234567Yuuuuui";
            var usuarioAgregado2 = repository.Update(usuario1);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual("1234567Yuuuuui", usuarioAgregado2.Contrasena);
        }

        [TestMethod]
        public void Eliminar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            repository.Delete(usuario1.Correo);
            var usuarioAgregado2 = repository.Find(u => u.Correo == usuario1.Correo);
            Assert.IsNull(usuarioAgregado2);
        }

        [TestMethod]
        public void Buscar_Usuario()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Find(u => u.Correo == usuario1.Correo);
            Assert.IsNotNull(usuarioAgregado2);
            Assert.AreEqual(usuario1, usuarioAgregado2);
        }

        [TestMethod]
        public void Buscar_Todos_Usuarios()
        {
            var usuario1 = new Usuario
            {
                Correo = "Juan@xxxx.com",
                Contrasena = "123456Yuuuuuuuu",
            };
            var usuario2 = new Usuario
            {
                Correo = "Jose@xxxxx.com",
                Contrasena = "1234567Yuuuuuu",
            };
            var repository = new UsuarioMemoryRepository();
            var usuarioAgregado1 = repository.Add(usuario1);
            var usuarioAgregado2 = repository.Add(usuario2);
            var usuarios = repository.FindAll();
            Assert.IsNotNull(usuarios);
            Assert.AreEqual(2, usuarios.Count);
        }
    }
}