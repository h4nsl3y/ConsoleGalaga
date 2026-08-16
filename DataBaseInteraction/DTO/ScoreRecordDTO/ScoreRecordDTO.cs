namespace DataLayer.DTO.ScoreRecordDTO
{
    public class ScoreRecordDTO : IScoreRecordDTO
    {
        #region Fields
        public int ScoreRecordId { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public DateTime LastModified { get; set; }
        #endregion

        #region Constructor
        public ScoreRecordDTO(int scoreRecordId, int playerId, int score, DateTime lastModified = default)
        {
            ScoreRecordId = scoreRecordId;
            PlayerId = playerId;
            Score = score;
            LastModified = lastModified == default ? DateTime.UtcNow : lastModified;
        }
        #endregion
    }
}
