﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions;
public class InternalServerException : DomainException
{
    public InternalServerException(string message) : base(message)
    {
    }
}
