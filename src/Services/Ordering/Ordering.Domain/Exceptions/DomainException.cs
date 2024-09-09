using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions;
public class DomainException : Exception
{
    public DomainException(string message) : base($"Exception {message} from Domain layer") { } 
}
