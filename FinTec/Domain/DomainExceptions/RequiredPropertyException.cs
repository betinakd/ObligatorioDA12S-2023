using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainExceptions
{
    public class RequiredPropertyException : Exception
    {
        public RequiredPropertyException(string message) : base(message)
        {
        }
    }
}
