namespace DTO
{
	public class EspacioDTO
	{
		public int Id { get; set; }	
		public string Nombre { get; set; }
		public UsuarioDTO Admin { get; set; }
		public List<UsuarioDTO> UsuariosInvitados { get; set; } = 
		public List<CategoriaDTO> Categorias { get; set; }
		public List<CambioDTO> Cambios { get; set; }
		public List<ObjetivoDTO> Objetivos { get; set; }
		public List<CuentaDTO> Cuentas { get; set; }
		public List<TransaccionDTO> Transacciones { get; set; }
	}
}
