

namespace BLL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Пользователь не найден.")
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}