namespace EnterTask.Data.Exceptions
{
    public class LinkNotFoundException : Exception
    {
        public int LinkId { get; set; }

        public LinkNotFoundException(string parent, string child, int id)
            : base($"Parent object {parent} for {child} with id [{id}] not found!")
        {
            LinkId = id;
        }
    }
}
