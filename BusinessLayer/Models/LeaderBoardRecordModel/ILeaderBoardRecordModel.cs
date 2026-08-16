namespace BusinessLayer.Models.LeaderBoardRecordModel
{
    public interface ILeaderBoardRecordModel
    {
        int Rank { get; set; }
        string Name { get; set; }
        int Score { get; set; }
    }
}
