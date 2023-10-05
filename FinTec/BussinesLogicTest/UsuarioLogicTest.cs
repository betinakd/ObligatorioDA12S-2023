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
		private IRepository<Usuario> repository;
		private UsuarioLogic usuarioLogic;
		private Usuario usuario1;
		private Usuario usuario2;

		[TestInitialize]
		public void Setup()
		{
			repository = new UsuarioMemoryRepository();
			usuarioLogic = new UsuarioLogic(repository);
			usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
		}


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
			var usuarioAgregado = repository.Find(u => u.Correo == usuario.Correo);
			bool resultado = usuario.Equals(usuarioAgregado);
			Assert.IsNotNull(usuarioAgregado);
			Assert.AreEqual(usuario.Correo, usuarioAgregado.Correo);
			Assert.AreEqual(usuario.Contrasena, usuarioAgregado.Contrasena);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
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
		[ExpectedException(typeof(DomainUsuarioException))]
		public void Correo_invalido_UL()
		{
			IRepository<Usuario> repository = new UsuarioMemoryRepository();
			UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.co";
			usuario1.Contrasena = "123456789Aa";
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

		[TestMethod]
		public void Eliminar_Usuario()
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
			usuarioLogic.DeleteUsuario(usuario1.Correo);
			var usuarios = usuarioLogic.FindAllUsuario();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(1, usuarios.Count);
		}

		[TestMethod]
		public void Actualizar_Usuario()
		{
			IRepository<Usuario> repository = new UsuarioMemoryRepository();
			UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xx@yy.com";
			usuario1.Contrasena = "123456780A";
			Usuario usuario2 = new Usuario();
			usuario2.Correo = "xxxx@yyyy.com";
			usuario2.Contrasena = "123456789A";
			usuarioLogic.AddUsuario(usuario1);
			usuarioLogic.AddUsuario(usuario2);
			usuario1.Contrasena = "123456789B";
			usuarioLogic.UpdateUsuario(usuario1);
			var usuarios = usuarioLogic.FindAllUsuario();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(2, usuarios.Count);
			Assert.AreEqual("123456789B", usuarios[0].Contrasena);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
		public void Actualizar_Usuario_Correo_Invalido()
		{
			IRepository<Usuario> repository = new UsuarioMemoryRepository();
			UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.com";
			usuario1.Contrasena = "123456780A";
			usuarioLogic.AddUsuario(usuario1);
			usuario1.Correo = "xxxx@yyyy.co";
			usuarioLogic.UpdateUsuario(usuario1);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainUsuarioException))]
		public void Actualizar_Usuario_Contrasena_Invalida()
		{
			IRepository<Usuario> repository = new UsuarioMemoryRepository();
			UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.com";
			usuario1.Contrasena = "123456780Ab";
			usuarioLogic.AddUsuario(usuario1);
			usuario1.Contrasena = "1234";
			usuarioLogic.UpdateUsuario(usuario1);
		}

		[TestMethod]
		public void Buscar_Usuario()
		{
			IRepository<Usuario> repository = new UsuarioMemoryRepository();
			UsuarioLogic usuarioLogic = new UsuarioLogic(repository);
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.com";
			usuario1.Contrasena = "123456780A";
			Usuario usuario2 = new Usuario();
			usuario2.Correo = "xx@yy.com";
			usuario2.Contrasena = "123456789A";
			usuarioLogic.AddUsuario(usuario1);
			usuarioLogic.AddUsuario(usuario2);
			var usuario = usuarioLogic.FindUsuario(usuario1.Correo);
			Assert.IsNotNull(usuario);
			Assert.AreEqual(usuario1.Correo, usuario.Correo);
			Assert.AreEqual(usuario1.Contrasena, usuario.Contrasena);
		}

		[TestMethod]
		public void Agregar_Usuario_Valido()
		{
			var usuario1 = new Usuario
			{
				Correo = "Juan@xxxx.com",
				Contrasena = "123456gggU__u",
			};
			var usuario2 = new Usuario
			{
				Correo = "Juann@xxxx.com",
				Contrasena = "123456ggggU",
			};
			var repository = new UsuarioMemoryRepository();
			var logica = new UsuarioLogic(repository);
			var usuarioAgregado1 = logica.AddUsuario(usuario1);
			var usuarioAgregado2 = logica.AddUsuario(usuario2);
			Assert.IsNotNull(usuarioAgregado1);
			Assert.AreEqual(usuario1, usuarioAgregado1);
			Assert.IsNotNull(usuarioAgregado2);
			Assert.AreEqual(usuario2, usuarioAgregado2);
		}


		[TestMethod]
		[ExpectedException(typeof(BussinesLogicUsuarioException))]
		public void Agregar_Usuario_Invalido_Duplicado()
		{
			var usuario1 = new Usuario
			{
				Correo = "Juan@xxxx.com",
				Contrasena = "123456aasaU",
			};
			var usuario2 = new Usuario
			{
				Contrasena = "123456aasaU",
				Correo = "Juan@xxxx.com"
			};
			var repository = new UsuarioMemoryRepository();
			var logica = new UsuarioLogic(repository);
			logica.AddUsuario(usuario1);
			logica.AddUsuario(usuario2);
		}

		[TestMethod]
		public void Existe_Usuario_Segun_Correo()
		{
			usuarioLogic.AddUsuario(usuario1);
			bool resultadoPositivo = usuarioLogic.ExisteCorreoUsuario("hola@gmail.com");
			bool resultadoNegativo = usuarioLogic.ExisteCorreoUsuario("holaaaaa@gmail.com");
			Assert.IsTrue(resultadoPositivo);
			Assert.IsFalse(resultadoNegativo);

		}

		[TestMethod]
		public void Ingreso_Correo_Contrasena_Valida_Entrega_Usuario()
		{
			usuarioLogic.AddUsuario(usuario1);
			usuarioLogic.AddUsuario(usuario2);
			Usuario usuario = usuarioLogic.UsuarioByCorreoContrasena("hola@gmail.com", "123456789Aaa");
			bool resultado = usuario.Equals(usuario1);
			bool contrasenaIgual = usuario.Contrasena == usuario1.Contrasena;
			Assert.IsTrue(resultado);
			Assert.IsTrue(contrasenaIgual);
		}

	}
}
