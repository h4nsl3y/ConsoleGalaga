namespace DataLayer.DTO.ScoreRecordDTO
{
    public interface IScoreRecordDTO
    {
        int ScoreRecordId { get; }
        int PlayerId { get; }
        int Score { get; set; }
        DateTime LastModified { get; }
    }
}
