namespace BusinessLayer.Models.AppDataModel
{
    public interface IAppUserModel
    {
        int UserId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
