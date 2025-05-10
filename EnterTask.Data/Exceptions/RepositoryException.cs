namespace EnterTask.Data.Exceptions
{
    public class RepositoryException : Exception
    {
        public object? Repository { get; }

        public RepositoryException()
            : base()
        {
        }

        public RepositoryException(object? repository)
            : base()
        {
            Repository = repository;
        }

        public RepositoryException(object? repository, string? message)
            : base(message)
        {
            Repository = repository;
        }

        public RepositoryException(object? repository, string? message, Exception? innerException)
            : base(message, innerException)
        {
            Repository = repository;
        }

    }
}
