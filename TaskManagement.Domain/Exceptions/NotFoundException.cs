using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string name, object key)
            : base($"{name} with id '{key}' was not found.")
        {
        }
    }
}
