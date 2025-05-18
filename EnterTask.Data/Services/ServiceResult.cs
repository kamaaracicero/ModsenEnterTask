namespace EnterTask.Data.Services
{
    public class ServiceResult
    {
        public string? Message { get; set; }

        public bool Success { get; set; }

        public Exception? Exception { get; set; }

        public static ServiceResult Ok(string? message)
            => new(true, message);

        public static ServiceResult Fail(string? message, Exception? ex = null)
            => new(false, message) {
                Exception = ex
            };

        public ServiceResult(bool success = false, string? message = null)
        {
            Success = success;
            Message = message;
            Exception = null;
        }

        public ServiceResult()
            : this(false, null)
        { }
    }

    public class ServiceResult<TValue> : ServiceResult
    {
        public TValue? Value { get; set; }

        public ServiceResult(TValue? value, bool success = false, string? message = null)
            : base(success, message)
        {
            Value = value;
        }

        public static ServiceResult<TValue> Ok(TValue value, string message)
            => new(value, true, message);

        public new static ServiceResult<TValue> Fail(string? message, Exception? ex = null)
            => new(default, false, message) {
                Exception = ex,
            };
    }
}
