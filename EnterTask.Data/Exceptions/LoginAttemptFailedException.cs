namespace EnterTask.Data.Exceptions
{
    public class LoginAttemptFailedException : Exception
    {
        public LoginAttemptFailedException()
            : base("Login or password incorrect")
        { }
    }
}
