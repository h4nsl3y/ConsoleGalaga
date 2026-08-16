namespace BusinessLayer.Models.ServiceResponse
{
    public interface IServiceResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        T Data { get; }
    }
}
