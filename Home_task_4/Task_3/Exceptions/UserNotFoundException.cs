namespace Task_3.Exceptions
{
    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException() : base()
        {

        }

        public UserNotFoundException(string? message) : base(message)
        {

        }
    }
}
