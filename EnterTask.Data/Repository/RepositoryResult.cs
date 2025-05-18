namespace EnterTask.Data.Repository
{
    [Obsolete]
    public class RepositoryResult
    {
        public List<Exception> Errors { get; set; }

        public bool Successfully { get; set; }

        public RepositoryResult(bool successfully = true)
        {
            Successfully = successfully;
            Errors = new List<Exception>();
        }
    }

    [Obsolete]
    public class RepositoryResult<TValue> : RepositoryResult
    {
        public TValue Value { get; set; }

        public RepositoryResult(TValue value, bool successfully = true)
            : base(successfully)
        {
            Value = value;
        }
    }
}
