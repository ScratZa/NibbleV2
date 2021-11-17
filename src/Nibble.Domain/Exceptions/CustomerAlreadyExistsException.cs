using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Exceptions
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(Guid id, string name) 
            : base(GenerateExceptionMessage(id, name))
        {

        }

        private static string GenerateExceptionMessage(Guid id, string name)
        {
            return $"The customer {name} with id : {id} already exists";
        }
    }
}
