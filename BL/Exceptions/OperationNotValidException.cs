﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class OperationNotValidException : Exception
    {
        public OperationNotValidException(string mes) : base(mes)
        {
            
        }
    }
}
