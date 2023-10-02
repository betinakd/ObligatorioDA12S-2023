using Domain;
using System.ComponentModel.DataAnnotations;

namespace DomainTest
{
	[TestClass]
	public class EspacioTest
	{
		[TestMethod]
		public void Nueva_Espacio_No_Nulo()
		{
			var espacio = new Espacio();
			Assert.IsNotNull(espacio);
		}


		[TestMethod]
		public void Espacio_Tiene_Admin()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			espacio.Admin = usuario;
			Assert.AreEqual(espacio.Admin, usuario);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Espacio_Tiene_Admin_Nulo()
		{
			var espacio = new Espacio();
			espacio.Admin = null;
		}

		[TestMethod]
		public void Espacio_Tiene_UsuariosInvitados()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			espacio.Admin = usuario;
			espacio.UsuariosInvitados = usuarios;
			Assert.AreEqual(espacio.UsuariosInvitados, usuarios);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Espacio_Admin_En_UsuariosInvitados()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			espacio.Admin = usuario;
			usuarios.Add(usuario);
			espacio.UsuariosInvitados = usuarios;
		}

		[TestMethod]
		public void Espacio_Invitar_Usuario()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			Usuario usuario2 = new Usuario();
			List<Usuario> resultado = new List<Usuario>();
			resultado.Add(usuario);
			List<Usuario> usuarios = new List<Usuario>();
			espacio.Admin = usuario2;
			espacio.UsuariosInvitados = usuarios;
			espacio.InvitarUsuario(usuario);
			CollectionAssert.AreEqual(espacio.UsuariosInvitados, resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Espacio_Invitar_Usuario_Ya_Presente()
		{
			var espacio = new Espacio();
			Usuario usuario = new Usuario();
			Usuario usuario2 = new Usuario();
			List<Usuario> usuarios = new List<Usuario>();
			usuarios.Add(usuario);
			espacio.Admin = usuario2;
			espacio.UsuariosInvitados = usuarios;
			espacio.InvitarUsuario(usuario);
		}

		[TestMethod]
		public void Espacio_Inicializa_Lista_Cuentas_Vacia()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			espacio.Admin = usuario;
			List<Cuenta> cuentas = espacio.Cuentas;
			Assert.IsInstanceOfType(cuentas, typeof(List<Cuenta>));
			Assert.IsNotNull(cuentas);
			Assert.AreEqual(cuentas.Count, 0);
		}

		[TestMethod]
		public void Espacio_Agregar_Cuenta()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			var cuenta = new Cuenta();
			espacio.Admin = usuario;
			espacio.AgregarCuenta(cuenta);
			Assert.AreEqual(espacio.Cuentas.Count, 1);
			Assert.AreEqual(espacio.Cuentas[0], cuenta);
		}

		[TestMethod]
		public void Espacio_Inicializa_Lista_Categorias_Vacia()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			espacio.Admin = usuario;
			List<Categoria> categorias = espacio.Categorias;
			Assert.IsInstanceOfType(categorias, typeof(List<Categoria>));
			Assert.IsNotNull(categorias);
			Assert.AreEqual(categorias.Count, 0);
		}

		[TestMethod]
		public void Espacio_Agregar_Categoria()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			var categoria = new Categoria();
			espacio.Admin = usuario;
			espacio.AgregarCategoria(categoria);
			Assert.AreEqual(espacio.Categorias.Count, 1);
			Assert.AreEqual(espacio.Categorias[0], categoria);
		}

		[TestMethod]
		public void Espacio_Inicializa_Lista_Transacciones_Vacia()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			espacio.Admin = usuario;
			List<Transaccion> transacciones = espacio.Transacciones;
			Assert.IsInstanceOfType(transacciones, typeof(List<Transaccion>));
			Assert.IsNotNull(transacciones);
			Assert.AreEqual(transacciones.Count, 0);
		}

		[TestMethod]
		public void Espacio_Agregar_Transaccion()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			var transaccion = new Transaccion();
			espacio.Admin = usuario;
			espacio.AgregarTransaccion(transaccion);
			Assert.AreEqual(espacio.Transacciones.Count, 1);
			Assert.AreEqual(espacio.Transacciones[0], transaccion);
		}

		[TestMethod]
		public void Espacio_Inicializa_Lista_Objetivos_Vacia()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			espacio.Admin = usuario;
			List<Objetivo> objetivos = espacio.Objetivos;
			Assert.IsInstanceOfType(objetivos, typeof(List<Objetivo>));
			Assert.IsNotNull(objetivos);
			Assert.AreEqual(objetivos.Count, 0);
		}
		[TestMethod]
		public void Espacio_Agregar_Objetivo()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			var objetivo = new Objetivo();
			espacio.Admin = usuario;
			espacio.AgregarObjetivo(objetivo);
			Assert.AreEqual(espacio.Objetivos.Count, 1);
			Assert.AreEqual(espacio.Objetivos[0], objetivo);
		}

		[TestMethod]
		public void Espacio_Inicializa_Lista_Cambio_Vacia()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			espacio.Admin = usuario;
			List<Cambio> cambios = espacio.Cambios;
			Assert.IsInstanceOfType(cambios, typeof(List<Cambio>));
			Assert.IsNotNull(cambios);
			Assert.AreEqual(cambios.Count, 0);
		}

		[TestMethod]
		public void Espacio_Agregar_Cambio()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			var cambio = new Cambio();
			espacio.Admin = usuario;
			espacio.AgregarCambio(cambio);
			Assert.AreEqual(espacio.Cambios.Count, 1);
			Assert.AreEqual(espacio.Cambios[0], cambio);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Agregar_Cuenta_Ahorro_Repetida()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			Ahorro ahorro1 = new Ahorro()
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Nombre = "Banco1",
			};
			Ahorro ahorro2 = new Ahorro()
			{
				Moneda = TipoCambiario.Dolar,
				Nombre = "Banco1",
			};
			espacio.AgregarCuenta(ahorro1);
			espacio.AgregarCuenta(ahorro2);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Agregar_Cuenta_Credito_Repetida()
		{
			var espacio = new Espacio();
			var usuario = new Usuario();
			Credito credito1 = new Credito()
			{
				BancoEmisor = "Banco1",
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			Credito credito2 = new Credito()
			{
				BancoEmisor = "Banco1",
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			espacio.AgregarCuenta(credito1);
			espacio.AgregarCuenta(credito2);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Se_Agrega_Admin_Como_Invitado()
		{
			var espacio = new Espacio();
			var usuario = new Usuario()
			{
				Correo = "mail@ejemplo.com",
				Contrasena = "123456789T",
			};
			espacio.Admin = usuario;
			espacio.InvitarUsuario(usuario);
		}
	}
}
