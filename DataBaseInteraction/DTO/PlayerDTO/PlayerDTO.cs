namespace DataLayer.DTO.PlayerDTO
{
    public class PlayerDTO : IPlayerDTO
    {
        #region Fields
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        #endregion

        #region Constructor
        public PlayerDTO(int playerId, string name, DateTime dateCreated = default)
        {
            PlayerId = playerId;
            PlayerName = name;
            DateCreated = dateCreated == default ? DateTime.Now : dateCreated;
        }
        #endregion
    }
}
