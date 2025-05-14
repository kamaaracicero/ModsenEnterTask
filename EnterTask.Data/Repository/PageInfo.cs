namespace EnterTask.Data.Repository
{
    public class PageInfo
    {
        public PageInfo(int page, int pageSize, int? totalPages, int? totalCount)
        {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }

        public PageInfo(int page, int pageSize)
            : this(page, pageSize, null, null)
        { }

        public PageInfo()
            : this(1, 1)
        { }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int? TotalPages { get; set; }

        public int? TotalCount { get; set; }
    }
}
