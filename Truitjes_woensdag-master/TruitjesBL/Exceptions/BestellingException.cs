using System.Runtime.Serialization;

namespace TruitjesBL.Exceptions
{
    public class BestellingException : Exception
    {

        public BestellingException(string? message) : base(message)
        {
        }

        public BestellingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}