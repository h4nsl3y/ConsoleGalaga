namespace BusinessLayer.Models.LeaderBoardRecordModel
{
    public class LeaderBoardRecordModel(string name, int score) : ILeaderBoardRecordModel
    {
        #region Fields
        public int Rank { get; set; } = 0;
        public string Name { get; set; } = name;
        public int Score { get; set; } = score;
        #endregion
    }
}