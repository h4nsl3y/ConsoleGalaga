namespace BusinessLayer.Models.AppDataModel
{
    public class AppUserModel(int userId, string username, string password = "") : IAppUserModel
    {
        #region Fields
        public int UserId { get; set; } = userId;
        public string Username { get; set; } = username;
        public string Password { get; set; } = password;

        #endregion
        #region Constructor
        #endregion
    }
}
