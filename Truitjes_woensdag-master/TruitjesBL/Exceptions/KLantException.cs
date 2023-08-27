using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruitjesBL.Exceptions
{
    public class KLantException : Exception
    {
        public KLantException(string? message) : base(message)
        {
        }

        public KLantException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
