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

        [TestMethod]
        public void Agregar_Usuario()
        {
            IRepository<Usuario> repository = new UsuarioMemoryRepository();
            UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
            Usuario usuario = new Usuario();
            usuario.Correo = "xxxx@yyyy.com";
            usuario.Contrasena = "123456789A";
            usuarioLogic.AddUsuario(usuario);
            var usuarioAgregado = repository.Find(u=> u.Correo == usuario.Correo);
            bool resultado = usuario.Equals(usuarioAgregado);
            Assert.IsNotNull(usuarioAgregado);
            Assert.AreEqual(usuario.Correo, usuarioAgregado.Correo);
            Assert.AreEqual(usuario.Contrasena, usuarioAgregado.Contrasena);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Contrasena_invalida_UL()
        {
            IRepository<Usuario> repository = new UsuarioMemoryRepository();
            UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
            Usuario usuario1 = new Usuario();
            usuario1.Correo = "xxxx@yyyy.com";
            usuario1.Contrasena = "1234567890";
            usuarioLogic.AddUsuario(usuario1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Correo_invalido_UL()
        {
            IRepository<Usuario> repository = new UsuarioMemoryRepository();
            UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
            Usuario usuario1 = new Usuario();
            usuario1.Correo = "xxxx@yyyy.co";
            usuario1.Contrasena = "123456789A";
            usuarioLogic.AddUsuario(usuario1);
        }

        [TestMethod]
        public void Buscar_Todos_Usuarios()
        {
            IRepository<Usuario> repository = new UsuarioMemoryRepository();
            UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
            Usuario usuario1 = new Usuario();
            usuario1.Correo = "xx@yy.com";
            usuario1.Contrasena = "123456789A";
            Usuario usuario2 = new Usuario();
            usuario2.Correo = "xxxx@yyyy.com";
            usuario2.Contrasena = "123456789A";
            usuarioLogic.AddUsuario(usuario1);
            usuarioLogic.AddUsuario(usuario2);
            var usuarios = usuarioLogic.FindAllUsuario();
            Assert.IsNotNull(usuarios);
            Assert.AreEqual(2, usuarios.Count);
        }
    }
}
