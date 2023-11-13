using Excepcion;
using System.Text.RegularExpressions;
namespace Domain
{
	public class Usuario
	{
		private string _contrasena;
		private string _correo;
		private string _nombre;
		private string _apellido;

		public int Id { get; set; }
		public List<Espacio> Espacios { get; set; }
		public List<Espacio> EspaciosAdmin { get; set; }
		public int IdEspacioPrincipal { get; set; }
		public string Direccion { get; set; }
		public string Contrasena
		{
			get
			{
				return _contrasena;
			}
			set
			{
				if (!Validar_Contrasena(value))
				{
					throw new DomainUsuarioException("La contraseña no es válida, debe contener al menos una mayúscula, largo mayor igual a 10 y menor igual a 30");
				}
				_contrasena = value;
			}
		}
		public string Correo
		{
			get { return _correo; }
			set
			{
				if (value is null)
				{
					throw new DomainUsuarioException("El correo electrónico es requerido");
				}
				if (Validar_Correo(value))
				{
					_correo = value;
				}
				else
				{
					throw new DomainUsuarioException("El correo electrónico no es válido, debe terminar en .com y tener @ entre carácteres.");
				}
			}
		}
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
					throw new DomainUsuarioException("El nombre es requerido");
				}
				_nombre = value;
			}
		}
		public string Apellido
		{
			get
			{
				return _apellido;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new DomainUsuarioException("El apellido es requerido");
				}
				_apellido = value;
			}
		}

		public bool Validar_Contrasena(string contrasena)
		{
			const int CONTRASENAMAXIMO = 30;
			const int CONTRASENAMINIMO = 10;

			if (contrasena.Length > CONTRASENAMAXIMO)
			{
				return false;
			}
			if (SonTodasMinusculas(contrasena))
			{
				return false;
			}

			return contrasena.Length >= CONTRASENAMINIMO;
		}

		public bool Validar_Correo(string correo)
		{
			if (!ContienePuntoCom(correo))
			{
				return false;
			}
			return ContieneArroba(correo);
		}

		private bool ContienePuntoCom(string correo)
		{
			string _patron = @".+\.com$";
			return Regex.IsMatch(correo, _patron);
		}

		private bool ContieneArroba(string correo)
		{
			string patron = @"^.+@.+$";
			return Regex.IsMatch(correo, patron);
		}

		private bool SonTodasMinusculas(string contrasena)
		{
			return contrasena.ToLower() == contrasena;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			};

			Usuario user = (Usuario)obj;
			return Correo == user.Correo;
		}
	}
}
