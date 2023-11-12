using DTO;
using DTO.EnumsDTO;
namespace DTOTest
{
	[TestClass]
	public class EspacioDTOTest
	{
		[TestMethod]
		public void EspacioDTO_Tiene_Nombre()
		{
			EspacioDTO espacio = new EspacioDTO();
			string nombre = "EspacioTest";
			espacio.Nombre = nombre;
			Assert.AreEqual(nombre, espacio.Nombre);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Admin()
		{
			EspacioDTO espacio = new EspacioDTO();
			string nombre = "EspacioTest";
			espacio.Nombre = nombre;
			UsuarioDTO admin = new UsuarioDTO
			{
				Nombre = "Juan",
				Apellido = "Lopez",
				Correo = "hola@gmail.com",
				Contrasena = "TestTest123",
				Direccion = "Av españa 4567"
			};
			espacio.Admin = admin;
			Assert.AreEqual(admin, espacio.Admin);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Id()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			int id = 1;
			espacioDTO.Id = id;
			Assert.AreEqual(id, espacioDTO.Id);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_UsuariosInvitados()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			List<UsuarioDTO> usuariosInvitados = new List<UsuarioDTO>();
			UsuarioDTO usuarioInvitado = new UsuarioDTO
			{
				Nombre = "Juan",
				Apellido = "Lopez",
				Correo = "test@gmail.com",
				Contrasena = "TestTest123",
				Direccion = "Av españa 4567"
			};
			usuariosInvitados.Add(usuarioInvitado);
			espacioDTO.UsuariosInvitados = usuariosInvitados;
			Assert.AreEqual(usuariosInvitados, espacioDTO.UsuariosInvitados);
			Assert.AreEqual(usuarioInvitado, espacioDTO.UsuariosInvitados[0]);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Categorias()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			List<CategoriaDTO> categorias = new List<CategoriaDTO>();
			CategoriaDTO categoria = new CategoriaDTO
			{
				Nombre = "CategoriaTest",
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoriaDTO.Ingreso
			};
			categorias.Add(categoria);
			espacioDTO.Categorias = categorias;
			Assert.AreEqual(categorias, espacioDTO.Categorias);
			Assert.AreEqual(categoria, espacioDTO.Categorias[0]);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Cambios()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			List<CambioDTO> cambios = new List<CambioDTO>();
			CambioDTO cambio = new CambioDTO()
			{
				Moneda = TipoCambiarioDTO.Dolar,
				FechaDeCambio = DateTime.Now,
				Pesos = 1000
			};
			cambios.Add(cambio);
			espacioDTO.Cambios = cambios;
			Assert.AreEqual(cambios, espacioDTO.Cambios);
			Assert.AreEqual(cambio, espacioDTO.Cambios[0]);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Objetivos()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			List<ObjetivoDTO> objetivos = new List<ObjetivoDTO>();
			var categorias = new List<CategoriaDTO>();
			CategoriaDTO categoria = new CategoriaDTO()
			{
				Nombre = "CategoriaTest",
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoriaDTO.Ingreso
			};
			categorias.Add(categoria);
			ObjetivoDTO objetivo = new ObjetivoDTO
			{
				Titulo = "ObjetivoTest",
				MontoMaximo = 1000,
				Categorias = categorias
			};
			objetivos.Add(objetivo);
			espacioDTO.Objetivos = objetivos;
			Assert.AreEqual(objetivos, espacioDTO.Objetivos);
			Assert.AreEqual(objetivo, espacioDTO.Objetivos[0]);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Cuentas()
		{
			EspacioDTO espacioDTO = new EspacioDTO();
			List<CuentaDTO> cuentas = new List<CuentaDTO>();
			AhorroDTO ahorro = new AhorroDTO()
			{
				FechaCreacion = DateTime.Now,
				Nombre = "AhorroTest",
				Monto = 1000,
				Moneda = TipoCambiarioDTO.Dolar,
			};
			cuentas.Add(ahorro);
			espacioDTO.Cuentas = cuentas;
			Assert.AreEqual(cuentas, espacioDTO.Cuentas);
			Assert.AreEqual(ahorro, espacioDTO.Cuentas[0]);
		}

		[TestMethod]
		public void EspacioDTO_Tiene_Transacciones()
		{
			AhorroDTO ahorro = new AhorroDTO()
			{
				FechaCreacion = DateTime.Now,
				Nombre = "AhorroTest",
				Monto = 1000,
				Moneda = TipoCambiarioDTO.Dolar,
			};
			CategoriaDTO categoria = new CategoriaDTO()
			{
				Nombre = "CategoriaTest",
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoriaDTO.Ingreso
			};
			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CategoriaTransaccion = "Costo",
				CuentaMonetaria = "Pesos Santander",
				FechaTransaccion = DateTime.Now,
				Id = 1,
				Monto = 1000,
				Titulo = "TransaccionTest"
			};
			EspacioDTO espacioDTO = new EspacioDTO();
			var transacciones = new List<TransaccionDTO>();
			transacciones.Add(transaccion);
			espacioDTO.Transacciones = transacciones;
			Assert.AreEqual(transacciones, espacioDTO.Transacciones);
			Assert.AreEqual(transaccion, espacioDTO.Transacciones[0]);
		}
	}
}
