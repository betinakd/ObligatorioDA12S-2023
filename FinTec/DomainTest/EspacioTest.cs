using Domain;
using System.ComponentModel.DataAnnotations;

namespace DomainTest
{
	[TestClass]
	public class EspacioTest
	{
		private Usuario usuario1;
		private Usuario usuario2;
		private Espacio espacio1;
		private Categoria categoria1;
		private Categoria categoria2;
		[TestInitialize]
		public void Setup()
		{


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

			espacio1 = new Espacio() { Admin = usuario1, Nombre = "Espacio1" };
			categoria1 = new Categoria() { Nombre = "Categoria1", Tipo = TipoCategoria.Costo, EstadoActivo = true };
			categoria2 = new Categoria() { Nombre = "Categoria2", Tipo = TipoCategoria.Costo, EstadoActivo = true };
		}
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
		[ExpectedException(typeof(DomainEspacioException))]

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
			var transaccion = new Transaccion()
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion",
				Monto = 100,
				CategoriaTransaccion = new Categoria()
				{
					EstadoActivo = true,
					Nombre = "Categoria",
					Tipo = TipoCategoria.Costo,
				},
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "Ahorro",
					Moneda = TipoCambiario.PesosUruguayos,
				},
			};
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

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Se_Agrega_Transaccion_Dolares_Sin_Cambio_Fecha()
		{
			Transaccion transaccion = new Transaccion()
			{
				Titulo = "Transaccion",
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
				CategoriaTransaccion = new Categoria()
				{
					EstadoActivo = true,
					Nombre = "Categoria",
					Tipo = TipoCategoria.Costo,
				},
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "Ahorro",
					Moneda = TipoCambiario.Dolar,
				},
			};
			Espacio espacio = new Espacio();
			espacio.AgregarTransaccion(transaccion);
		}

		[TestMethod]
		public void Excepcion_Se_Agrega_Transaccion_Dolares_Con_Cambio_Fecha()
		{
			Transaccion transaccion = new Transaccion()
			{
				Titulo = "Transaccion",
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
				CategoriaTransaccion = new Categoria()
				{
					EstadoActivo = true,
					Nombre = "Categoria",
					Tipo = TipoCategoria.Costo,
				},
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "Ahorro",
					Moneda = TipoCambiario.Dolar,
				},
			};
			Espacio espacio = new Espacio();
			Cambio cambio = new Cambio();
			espacio.AgregarCambio(cambio);
			espacio.AgregarTransaccion(transaccion);
			Assert.AreEqual(espacio.Transacciones.Count, 1);
		}

		[TestMethod]
		public void Espacio_Tiene_Nombre()
		{
			var espacio = new Espacio();
			espacio.Nombre = "Espacio";
			Assert.AreEqual(espacio.Nombre, "Espacio");
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Espacio_Tiene_Nombre_Nulo()
		{
			var espacio = new Espacio();
			espacio.Nombre = null;
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Espacio_Tiene_Nombre_Vacio()
		{
			var espacio = new Espacio();
			espacio.Nombre = null;
		}

		[TestMethod]
		public void Static_Contador_De_Id()
		{
			var espacio = new Espacio();
			var espacio2 = new Espacio();
			espacio.Id = 1;
			espacio2.Id = 2;
		}

		[TestMethod]
		public void Recibe_Correo_Retorna_True_Si_Usuario_Pertenece_Espacio()
		{
			var espacio = new Espacio()
			{
				Admin = new Usuario()
				{
					Correo = "holaaaa@gmail.com",
					Contrasena = "123456789A",
				}
			};
			bool resultado = espacio.PerteneceCorreo("holaaaa@gmail.com");
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		public void Borrar_Categoria_Espacio()
		{
			espacio1.AgregarCategoria(categoria1);
			espacio1.BorrarCategoria(categoria1);
			Assert.AreEqual(espacio1.Categorias.Count, 0);
		}




		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Borrar_Categoria_Asociada_Transaccion()
		{
			espacio1.AgregarCategoria(categoria1);
			Transaccion transaccion = new Transaccion()
			{
				Titulo = "Transaccion",
				Moneda = TipoCambiario.Dolar,
				Monto = 100,
				CategoriaTransaccion = categoria1,
				CuentaMonetaria = new Ahorro()
				{
					Nombre = "Ahorro",
					Moneda = TipoCambiario.Dolar,
				},
			};
			espacio1.AgregarTransaccion(transaccion);
			espacio1.BorrarCategoria(categoria1);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainEspacioException))]
		public void Excepcion_Recibe_Cambio_Dos_Fechas_Iguales()
		{
			Cambio cambio1 = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 100,
				FechaDeCambio = new System.DateTime(2018, 10, 10),
			};
			Cambio cambio2 = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 10,
				FechaDeCambio = new System.DateTime(2018, 10, 10),
			};
			espacio1.AgregarCambio(cambio1);
			espacio1.AgregarCambio(cambio2);
		}
	}
}
