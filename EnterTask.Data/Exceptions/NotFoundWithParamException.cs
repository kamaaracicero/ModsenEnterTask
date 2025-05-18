namespace EnterTask.Data.Exceptions
{
    public class NotFoundWithParamException : Exception
    {
        public object SearchParam { get; set; }

        public NotFoundWithParamException(string searchEntity, object searchParam)
            : base($"{searchEntity} with param {searchParam} not found!\nEntity may be already deleted.")
        {
            SearchParam = searchParam;
        }

    }
}
