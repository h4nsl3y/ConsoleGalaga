namespace SVC_API.Models.Response
{
    public interface IResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        T? Data { get; }
    }
}
