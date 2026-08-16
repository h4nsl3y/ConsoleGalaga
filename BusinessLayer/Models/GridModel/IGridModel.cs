namespace BusinessLayer.Models.GridModel
{
    public interface IGridModel<T>
    {
        IList<string> Titles { get; }
        IList<T> Records { get; }
        int CurrentPage { get; }
        int TotalPages { get; }
        int TotalRecords { get; }
    }
}
