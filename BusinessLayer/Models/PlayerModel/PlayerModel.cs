namespace BusinessLayer.Models.PlayerModel
{
    public class PlayerModel(int playerId, string name, DateTime dateCreated = default) : IPlayerModel
    {
        public int PlayerId { get; set; } = playerId;
        public string PlayerName { get; set; } = name;
        public DateTime DateCreated { get; set; } = dateCreated == default ? DateTime.Now : dateCreated;
    }
}
