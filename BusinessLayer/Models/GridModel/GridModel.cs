namespace BusinessLayer.Models.GridModel
{
    public class GridModel<T>(IList<string> titles, IList<T> records, int page, int totalCount, int pageSize = 10) : IGridModel<T>
    {
        public IList<string> Titles { get; } = titles;
        public IList<T> Records { get; } = records;
        public int CurrentPage { get; } = page;
        public int TotalPages { get; } = (int)Math.Ceiling(totalCount / (double)pageSize);
        public int TotalRecords { get; } = totalCount;
    }
}