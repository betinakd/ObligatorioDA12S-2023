using Models.Excepcion;

namespace Models
{

	namespace Domain
	{
		public class Espacio
		{

			public Espacio()
			{
			}

			private static int _contadorId = 1;
			public int Id { get; set; }
			private Usuario _admin;
			private List<Cuenta> _cuentas = new List<Cuenta>();
			private List<Categoria> _categorias = new List<Categoria>();
			private List<Usuario> _usuariosInvitados = new List<Usuario>();
			private List<Transaccion> _transacciones = new List<Transaccion>();
			private List<Objetivo> _objetivos = new List<Objetivo>();
			private List<Cambio> _cambios = new List<Cambio>();
			private string _nombre;
			public string Nombre
			{
				get
				{
					return _nombre;
				}
				set
				{
					if (string.IsNullOrEmpty(value))
					{
						throw new DomainEspacioException("El espacio debe tener un nombre");
					}
					_nombre = value;
				}
			}
			public List<Cambio> Cambios
			{
				get
				{
					return _cambios;
				}
			}
			public List<Objetivo> Objetivos
			{
				get
				{
					return _objetivos;
				}
			}
			public List<Transaccion> Transacciones
			{
				get
				{
					return _transacciones;
				}
			}
			public List<Categoria> Categorias
			{
				get
				{
					return _categorias;
				}
			}
			public List<Cuenta> Cuentas
			{
				get
				{
					return _cuentas;
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

			public void InvitarUsuario(Usuario usuario)
			{
				if (UsuariosInvitados.Contains(usuario) || usuario.Equals(Admin))
					throw new DomainEspacioException("El usuario ya se encuentra presente en el espacio.");
				UsuariosInvitados.Add(usuario);
			}

			public void AgregarCuenta(Cuenta cuenta)
			{
				if (Cuentas.Contains(cuenta))
					throw new DomainEspacioException("La cuenta ya esta agregada");
				_cuentas.Add(cuenta);
			}
			public void AgregarCategoria(Categoria categoria)
			{
				if (Categorias.Contains(categoria))
					throw new DomainEspacioException("No se pueden agregar dos categorías con el mismo nombre.");
				_categorias.Add(categoria);
			}
			public void AgregarTransaccion(Transaccion transaccion)
			{
				Cambio cambioHoy = new Cambio();
				if (!_cambios.Contains(cambioHoy) && transaccion.Moneda == TipoCambiario.Dolar)
					throw new DomainEspacioException("No hay cotización cambiaria de dolar para la fecha de hoy");
				_transacciones.Add(transaccion);
			}

			public void AgregarObjetivo(Objetivo objetivo)
			{
				_objetivos.Add(objetivo);
			}

			public void AgregarCambio(Cambio cambio)
			{
				if (_cambios.Contains(cambio))
					throw new DomainEspacioException("Ya existe un cambio para la fecha.");
				_cambios.Add(cambio);
			}

			public bool PerteneceCorreo(string correo)
			{
				return (Admin.Correo == correo || UsuariosInvitados.Any(u => u.Correo == correo));
			}

			public void BorrarCategoria(Categoria categoria)
			{
				if (Categorias.Contains(categoria))
				{
					if (TransaccionesContieneCategoria(categoria))
						throw new DomainEspacioException("No se puede borrar una categoría que tiene transacciones asociadas");
					if (CategoriaAsociadaObjetivos(categoria))
						throw new DomainEspacioException("No se puede borrar una categoría que asociada a algún objetivo.");
					Categorias.Remove(categoria);
				}
			}
			public void BorrarCuenta(Cuenta cuenta)
			{
				if (Cuentas.Contains(cuenta))
				{
					if (TransaccionesContieneCuenta(cuenta))
						throw new DomainEspacioException("No se puede borrar una categoría que tiene transacciones asociadas");
					Cuentas.Remove(cuenta);
				}
			}

			public bool TransaccionesContieneCuenta(Cuenta cuenta)
			{
				return (Transacciones.Any(t => t.CuentaMonetaria.Equals(cuenta)));
			}

			public bool CategoriaAsociadaObjetivos(Categoria categoria)
			{
				foreach (var objetivo in _objetivos)
				{
					if (objetivo.ContieneCategoria(categoria))
					{
						return true;
					}
				}
				return false;
			}

			public bool TransaccionesContieneCategoria(Categoria categoria)
			{
				return (Transacciones.Any(t => t.CategoriaTransaccion.Equals(categoria)));
			}
			public static void AumentarContadorId()
			{
				_contadorId++;
			}

			public void AsignarId()
			{
				Id = _contadorId;
				AumentarContadorId();
			}

			public void ModificarCuenta(Cuenta modificacion, Cuenta modificada)
			{
				if (Cuentas.Contains(modificacion))
				{
					throw new DomainEspacioException("No se puede Modificar, hay cuentas ya registradas con ese nombre");
				}
				modificada.Modificar(modificacion);
			}
		}
	}
}
