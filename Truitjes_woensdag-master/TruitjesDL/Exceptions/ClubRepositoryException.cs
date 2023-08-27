using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruitjesDL.Exceptions
{
    public class ClubRepositoryException : Exception
    {
        public ClubRepositoryException(string? message) : base(message)
        {
        }

        public ClubRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
