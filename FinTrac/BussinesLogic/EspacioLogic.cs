using Domain;
using Repository;
using Excepcion;


namespace BussinesLogic
{
	public class EspacioLogic
	{
		private readonly IRepository<Espacio> _repository;

		public EspacioLogic(IRepository<Espacio> repository)
		{
			_repository = repository;
		}

		public Espacio AddEspacio(Espacio oneElement)
		{
			IList<Espacio> espacios = _repository.FindAll();
			bool existe = espacios.Contains(oneElement);
			if (existe)
			{
				throw new BusinessLogicEspacioException("El espacio ya existe");
			}
			_repository.Add(oneElement);
			return oneElement;
		}
		public List<Espacio> EspaciosByCorreo(string correo)
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
		public void DeleteEspacio(Espacio oneElement)
		{
			_repository.Delete(oneElement.Admin.Correo);
		}

		public IList<Espacio> FindAllEspacios()
		{
			return _repository.FindAll();
		}

		public Espacio FindEspacio(int id)
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
			AddEspacio(espacio);
		}

		public void ModificarNombreEspacio(int espacioId, string nuevoNombre)
		{
			Espacio espacio = FindEspacio(espacioId);
			espacio.Nombre = nuevoNombre;
			UpdateEspacio(espacio);
		}

		public void AgregarUsuarioAEspacio(int idEspacio, Usuario usuario)
		{
			Espacio espacio = FindEspacio(idEspacio);
			espacio.InvitarUsuario(usuario);
			UpdateEspacio(espacio);
		}
	}
}
