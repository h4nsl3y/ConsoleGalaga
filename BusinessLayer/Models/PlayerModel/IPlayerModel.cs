namespace BusinessLayer.Models.PlayerModel
{
    public interface IPlayerModel
    {
        int PlayerId { get; set; }
        string PlayerName { get; set; }
        DateTime DateCreated { get; set; }
    }
}
