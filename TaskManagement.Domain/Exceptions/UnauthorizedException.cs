using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }

        public UnauthorizedException() : base("Unauthorized access.")
        {
        }
    }
}
