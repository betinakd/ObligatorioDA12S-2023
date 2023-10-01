namespace Domain
{
	public class Espacio
	{
		private Usuario _admin;
		public List<Usuario> UsuariosInvitados { get; set; }
		public Usuario Admin
		{
			get
			{ 
				return _admin;
			}
			set 
			{ 
				if(value == null)
					throw new DomainEspacioException("El espacio debe tener un administrador");
				_admin = value;
			}
		}
		public Espacio()
		{
		}
	}
}
