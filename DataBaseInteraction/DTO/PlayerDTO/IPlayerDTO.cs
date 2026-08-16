namespace DataLayer.DTO.PlayerDTO
{
    public interface IPlayerDTO
    {
        int PlayerId { get; }
        string PlayerName { get; set; }
        DateTime DateCreated { get; }
    }
}
