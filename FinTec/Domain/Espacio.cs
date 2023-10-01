namespace Domain
{
	public class Espacio
	{
		private Usuario _admin;
		private List<Cuenta> cuentas = new List<Cuenta>();
		private List<Usuario> _usuariosInvitados = new List<Usuario>();

		public List<Cuenta> Cuentas
		{
			get
			{
				return cuentas;
			}
		}
		public List<Usuario> UsuariosInvitados
		{
			get
			{
				return _usuariosInvitados;
			}
			set
			{
				if (value.Contains(_admin))
					throw new DomainEspacioException("El administrador no puede estar en la lista de invitados");
				_usuariosInvitados = value;
			}
		}
		public Usuario Admin
		{
			get
			{
				return _admin;
			}
			set
			{
				if (value == null)
					throw new DomainEspacioException("El espacio debe tener un administrador");
				_admin = value;
			}
		}
		public Espacio()
		{
		}

		public void cambiarAdmin(Usuario nuevoAdmin)
		{
			if (!UsuariosInvitados.Contains(nuevoAdmin))
				throw new DomainEspacioException("Solo puede cambiar a administrador a alguien presente en lista de usuarios del espacio");
			UsuariosInvitados.Add(Admin);
			Admin = nuevoAdmin;
			UsuariosInvitados.Remove(nuevoAdmin);
		}

		public void InvitarUsuario(Usuario usuario)
		{
			if (UsuariosInvitados.Contains(usuario))
				throw new DomainEspacioException("El usuario ya esta invitado");
			UsuariosInvitados.Add(usuario);
		}
	}
}
