﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string mes) : base(mes)
        {
            
        }
    }
}
