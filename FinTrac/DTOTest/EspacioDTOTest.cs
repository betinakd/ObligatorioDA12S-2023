using DTO;
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
	}
}
