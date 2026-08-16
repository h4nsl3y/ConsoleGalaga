namespace DataLayer.DTO.DatabaseResponse
{
    public interface IDatabaseResponse<T>
    {
        bool Success { get; }
        string Message { get; }
        T Data { get; }
    }
}
