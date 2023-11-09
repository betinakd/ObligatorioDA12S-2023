namespace DTO
{
	public class EspacioDTO
	{
		public int Id { get; set; }	
		public string Nombre { get; set; }
		public UsuarioDTO Admin { get; set; }
		public List<UsuarioDTO> UsuariosInvitados { get; set; }
	}
}
