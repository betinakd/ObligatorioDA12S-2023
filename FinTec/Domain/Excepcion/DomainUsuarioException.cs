namespace Models
{
    namespace Excepcion
    {
        public class DomainUsuarioException : Exception
        {
            public DomainUsuarioException(string message) : base(message)
            {
            }
        }
    }
}
