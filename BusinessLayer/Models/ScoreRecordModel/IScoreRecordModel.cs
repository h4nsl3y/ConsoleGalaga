namespace BusinessLayer.Models.ScoreRecordModel
{
    public interface IScoreRecordModel
    {
        int ScoreRecordId { get; }
        int PlayerId { get; }
        int Score { get; set; }
        DateTime LastModified { get; }
    }
}
