using Dominio;
using Repositorio;
using Excepcion;


namespace LogicaNegocio
{
	public class EspacioLogica
	{
		private readonly IRepositorio<Espacio> _repository;

		public EspacioLogica(IRepositorio<Espacio> repository)
		{
			_repository = repository;
		}

		public Espacio AgregarEspacio(Espacio oneElement)
		{
			IList<Espacio> espacios = _repository.FindAll();
			bool existe = espacios.Contains(oneElement);
			if (existe)
			{
				throw new LogicaNegocioEspacioExcepcion("El espacio ya existe");
			}
			_repository.Add(oneElement);
			return oneElement;
		}
		public List<Espacio> EspaciosPorCorreo(string correo)
		{
			IList<Espacio> espacios = _repository.FindAll();
			List<Espacio> espaciosUsuario = new List<Espacio>();
			foreach (Espacio espacio in espacios)
			{
				if (espacio.PerteneceCorreo(correo))
				{
					espaciosUsuario.Add(espacio);
				}
			}
			return espaciosUsuario;
		}
		public void BorrarEspacio(Espacio oneElement)
		{
			_repository.Delete(oneElement.Admin.Correo);
		}

		public IList<Espacio> DarEspacios()
		{
			return _repository.FindAll();
		}

		public Espacio EncontrarEspacio(int id)
		{
			return _repository.Find(e => e.Id == id);

		}

		public void UpdateEspacio(Espacio updateEntity)
		{
			_repository.Update(updateEntity);
		}

		public int EspacioMayorId()
		{
			Espacio espacioConMayorId = _repository.FindAll().OrderByDescending(e => e.Id).FirstOrDefault();
			if (espacioConMayorId == null)
			{
				return 0;
			}
			return espacioConMayorId.Id;
		}

		public void CrearEspacio(string nombre, Usuario admin)
		{
			Espacio espacio = new Espacio() { Nombre = nombre, Admin = admin };
			AgregarEspacio(espacio);
		}

		public void ModificarNombreEspacio(int espacioId, string nuevoNombre)
		{
			Espacio espacio = EncontrarEspacio(espacioId);
			espacio.Nombre = nuevoNombre;
			UpdateEspacio(espacio);
		}

		public void AgregarUsuarioAEspacio(int idEspacio, Usuario usuario)
		{
			Espacio espacio = EncontrarEspacio(idEspacio);
			espacio.InvitarUsuario(usuario);
			UpdateEspacio(espacio);
		}

		public void EliminarUsuarioDeEspacio(int idEspacio, Usuario usuario)
		{
			Espacio espacio = EncontrarEspacio(idEspacio);
			if (espacio.PerteneceCorreo(usuario.Correo))
			{
				espacio.UsuariosInvitados.Remove(usuario);
			}
			UpdateEspacio(espacio);
		}

		public void EliminarCuentaDeEspacio(int idEspacio, Cuenta cuenta)
		{
			Espacio espacio = EncontrarEspacio(idEspacio);
			espacio.BorrarCuenta(cuenta);
			UpdateEspacio(espacio);
		}

		public void ModificarCuentaDeEspacio(int idEspacio, Cuenta cuenta)
		{
			Espacio espacio = EncontrarEspacio(idEspacio);
			espacio.ModificarCuenta(cuenta);
			UpdateEspacio(espacio);
		}

		public void CrearCuenta(int idEspacio, Cuenta cuenta)
		{
			if (cuenta.Saldo <= 0)
			{
				throw new LogicaNegocioEspacioExcepcion("El saldo de la cuenta debe ser mayor a 0");
			}
			Espacio espacio = EncontrarEspacio(idEspacio);
			espacio.AgregarCuenta(cuenta);
			UpdateEspacio(espacio);
		}

		public void CrearTransaccion(int idEspacio, Transaccion transaccion)
		{
			Espacio espacio = EncontrarEspacio(idEspacio);
			espacio.AgregarTransaccion(transaccion);
			transaccion.EjecutarTransaccion();
			UpdateEspacio(espacio);
		}
	}
}
