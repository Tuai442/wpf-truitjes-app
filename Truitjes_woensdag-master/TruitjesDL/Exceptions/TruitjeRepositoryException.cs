using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruitjesDL.Exceptions
{
    public class TruitjeRepositoryException : Exception
    {
        public TruitjeRepositoryException(string? message) : base(message)
        {
        }

        public TruitjeRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
