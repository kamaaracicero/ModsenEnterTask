namespace EnterTask.Data.Exceptions
{
    public class NotFoundWithIdException : Exception
    {
        public object[] SearchKeys { get; set; }

        public NotFoundWithIdException(string searchEntity, params object[] keyValues)
            : base($"{searchEntity} with id {string.Join(",", keyValues)} not found!\nEntity may be already deleted.")
        {
            SearchKeys = keyValues;
        }
    }
}
