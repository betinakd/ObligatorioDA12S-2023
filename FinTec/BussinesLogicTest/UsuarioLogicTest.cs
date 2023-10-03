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
            usuario.Contrasena = "1234";
            usuarioLogic.AddUsuario(usuario);
            var usuarioAgregado = repository.Find(u=> u.Correo == usuario.Correo);
            bool resultado = usuario.Equals(usuarioAgregado);
            Assert.IsNotNull(usuarioAgregado);
            Assert.AreEqual(usuario.Correo, usuarioAgregado.Correo);
            Assert.AreEqual(usuario.Contrasena, usuarioAgregado.Contrasena);
            Assert.IsTrue(resultado);
        }
    }
}
