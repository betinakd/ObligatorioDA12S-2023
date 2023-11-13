namespace DTO
{
	public class EspacioDTO
	{
		public int Id { get; set; }	
		public string Nombre { get; set; }
		public UsuarioDTO Admin { get; set; }
		public List<UsuarioDTO> UsuariosInvitados { get; set; } = new List<UsuarioDTO>();
		public List<CategoriaDTO> Categorias { get; set; } = new List<CategoriaDTO>();
		public List<CambioDTO> Cambios { get; set; } = new List<CambioDTO>();
		public List<ObjetivoDTO> Objetivos { get; set; } = new List<ObjetivoDTO>();
		public List<CuentaDTO> Cuentas { get; set; } = new List<CuentaDTO>();
		public List<TransaccionDTO> Transacciones { get; set; } = new List<TransaccionDTO>();
	}
}
