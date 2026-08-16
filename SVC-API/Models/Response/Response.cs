namespace SVC_API.Models.Response
{
    public class Response<T> : IResponse<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T? Data { get; }

        private Response(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Response<T> SuccessResult(T data, string message = "")
        {
            return new Response<T>(true, message, data);
        }

        public static Response<T> FailureResult(string message)
        {
            return new Response<T>(false, message, default);
        }
    }
}
