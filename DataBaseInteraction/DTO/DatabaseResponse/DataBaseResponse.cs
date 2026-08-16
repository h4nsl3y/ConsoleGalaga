namespace DataLayer.DTO.DatabaseResponse
{
    public class DatabaseResponse<T> : IDatabaseResponse<T>
    {
        #region Fields
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }
        #endregion

        #region Constructor
        private DatabaseResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        #endregion

        #region Public Methods
        public static DatabaseResponse<T> SuccessResult(T data, string message = "")
        {
            return new DatabaseResponse<T>(true, message, data);
        }

        public static DatabaseResponse<T> FailureResult(string message)
        {
            return new DatabaseResponse<T>(false, message, default!);
        }
        #endregion
    }
}
