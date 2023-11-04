using Excepcion;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Domain
{
	public class Usuario
	{
		public int Id { get; set; }
		public List<Espacio> Espacios { get; set; }
		public List<Espacio> EspaciosAdmin { get; set; }
		public int IdEspacioPrincipal { get; set; }
		private string _contrasena;
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
					throw new DomainUsuarioException("La contraseña no es válida");
				}
				_contrasena = value;
			}
		}
		private string _correo;
		public string Correo
		{
			get { return _correo; }
			set
			{
				if (Validar_Correo(value))
				{
					_correo = value;
				}
				else
				{
					throw new DomainUsuarioException("El correo electrónico no es válido");
				}
			}
		}

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
					throw new DomainUsuarioException("El nombre es requerido");
				}
				_nombre = value;
			}
		}

		private string _apellido;
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
			if (EsContrasenaMayorATreinta(contrasena))
			{
				return false;
			}

			if (SonTodasMinusculas(contrasena))
			{
				return false;
			}

			return EsContrasenaMayorIgualADiez(contrasena);
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

		private bool EsContrasenaMayorIgualADiez(string contrasena)
		{
			return contrasena.Length >= 10;
		}

		private bool EsContrasenaMayorATreinta(string contrasena)
		{
			return contrasena.Length > 30;
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
