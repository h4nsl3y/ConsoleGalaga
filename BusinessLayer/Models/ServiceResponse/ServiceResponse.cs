namespace BusinessLayer.Models.ServiceResponse
{
    public class ServiceResponse<T>(bool success, string message, T data) : IServiceResponse<T>
    {
        #region Fields
        public bool Success { get; } = success;
        public string Message { get; } = message;
        public T Data { get; } = data;
        #endregion

        #region Public Methods
        public static ServiceResponse<T> SuccessResult(T data, string message = "")
        {
            return new ServiceResponse<T>(true, message, data);
        }

        public static ServiceResponse<T> FailureResult(string message)
        {
            return new ServiceResponse<T>(false, message, default!);
        }
        #endregion
    }
}
