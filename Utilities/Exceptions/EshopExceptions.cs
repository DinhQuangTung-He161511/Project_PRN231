using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exceptions
{
    public class EshopExceptions :Exception
    {
        public  EshopExceptions() { }
        public EshopExceptions(string message) 
            :base(message) 
        {
        
        }
        public EshopExceptions(Exception inner, string message) 
            : base(message, inner) 
        {

        }
    }
}
