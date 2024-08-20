using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions;
public class ErrorValidationException : DomainException
{
    public ErrorValidationException(string message) : base(message)
    {
    }
}
