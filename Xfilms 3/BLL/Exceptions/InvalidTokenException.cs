

namespace BLL.Exceptions
{

    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("Неверный токен или срок действия истек.")
        {
        }

        public InvalidTokenException(string message) : base(message)
        {
        }
    }
}