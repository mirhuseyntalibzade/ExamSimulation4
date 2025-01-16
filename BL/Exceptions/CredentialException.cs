using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Exceptions
{
    public class CredentialException : Exception
    {
        public CredentialException(string mes ) : base(mes)
        {
            
        }
    }
}
