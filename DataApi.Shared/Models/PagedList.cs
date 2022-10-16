namespace DataApi.Shared.Models
{
    public class PagedList<T>
    {
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public List<T> Records { get; set; }

        public PagedList(IEnumerable<T> data, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            PrepareData(data, page, pageSize);
        }

        public PagedList()
        {
            Page = 1;
            PageSize = 12;
        }

        private void PrepareData(IEnumerable<T> data, int page, int pageSize)
        {
            Records = Records ?? new List<T>();
            Records.Clear();
            var pageData = data.Skip((page - 1) * pageSize).Take(pageSize);
            Records.AddRange(pageData);

            ItemsCount = data.Count();
            TotalPages = ItemsCount / PageSize;
            if ((ItemsCount % PageSize) > 0)
                TotalPages++;

        }
    }
}
