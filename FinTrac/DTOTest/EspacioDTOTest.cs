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
	}
}
