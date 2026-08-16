namespace DataLayer.DTO.AppuserDTO
{
    public class AppUserDTO : IAppUserDTO
    {
        #region Fields
        public int UserId {  get; set; }
        public string Username { get; set; }
        #endregion

        #region Constructor
        public AppUserDTO(int userId, string userName)
        {
            UserId = userId;
            Username = userName;
        }
        #endregion
    }
}
