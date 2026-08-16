namespace SVC_API.Models
{
    public class ErrorViewModel
    {
        #region Fields
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        #endregion
    }
}
