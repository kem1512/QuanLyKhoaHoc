namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class PagingModel<T>
    {
        public IReadOnlyCollection<T> Items { get; }

        public int PageNumber { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public int PageSize { get; }

        public PagingModel(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}
