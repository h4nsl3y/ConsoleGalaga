namespace BusinessLayer.Models.ScoreRecordModel
{
    public class ScoreRecordModel(int scoreRecordId, int playerId, int score, DateTime lastModified = default) : IScoreRecordModel
    {
        #region Fields
        public int ScoreRecordId { get; set; } = scoreRecordId;
        public int PlayerId { get; set; } = playerId;
        public int Score { get; set; } = score;
        public DateTime LastModified { get; set; } = lastModified == default ? DateTime.UtcNow : lastModified;
        #endregion
    }
}
