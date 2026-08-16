using BusinessLayer.BL.PlayerBL;
using BusinessLayer.BL.ScoreRecordBL;
using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.LeaderBoardRecordModel;
using BusinessLayer.Models.ServiceResponse;
using DataLayer.DTO.ScoreRecordDTO;
using Helpers.LoggerHelper;

namespace BusinessLayer.Service.LeaderBoardService
{
    public class LeaderBoardService(IPlayerBL playerBL, IScoreRecordBL scoreRecordBL, ILogger logger) : ILeaderBoardService
    {
        #region Public Methods
        public async Task<IServiceResponse<bool>> AddScoreAsync(string playerName, int score)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                return ServiceResponse<bool>.FailureResult("Player name cannot be empty.");
            if (score < 0)
                return ServiceResponse<bool>.FailureResult("Score cannot be negative.");

            var playerResult = await playerBL.AddPlayerAsync(playerName);
            if (!playerResult.Success)
            {
                return ServiceResponse<bool>.FailureResult(playerResult.Message);
            }

            int playerId = playerResult.Data;
            var scoreResult = await scoreRecordBL.AddScoreRecordAsync(playerId, score);

            if (!scoreResult.Success)
            {
                return ServiceResponse<bool>.FailureResult(scoreResult.Message);
            }

            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<IGridModel<ILeaderBoardRecordModel>>> GetTopScoresAsync(int page = 0, int limit = 10)
        {
            limit = (limit <= 0) ? 10 : limit;
            page = (page < 0) ? 0 : page;

            var countResult = await scoreRecordBL.GetTopScoreRecordsCountAsync();
            int totalCount = countResult.Success ? countResult.Data : 0;

            var topScores = await scoreRecordBL.GetTopScoreRecordsAsync(page, limit);

            if (!topScores.Success)
            {
                return ServiceResponse<IGridModel<ILeaderBoardRecordModel>>.FailureResult(topScores.Message);
            }
            var records = topScores.Data;
            List<ILeaderBoardRecordModel> leaderBoardRecords = [];

            records = records == null ? new List<IScoreRecordDTO>() : records;

            int rankOffset = page * limit;
            foreach (var record in records)
            {
                leaderBoardRecords.Add(await GetPlayerRecord(record));
            }
            leaderBoardRecords = SortPlayerRecords(leaderBoardRecords, rankOffset);

            var gridTitle = new List<string>
            {
                "Rank",
                "Player PlayerName",
                "Score"
            };

            var leaderBoard = new GridModel<ILeaderBoardRecordModel>(gridTitle, leaderBoardRecords, page, totalCount, limit);

            return ServiceResponse<IGridModel<ILeaderBoardRecordModel>>.SuccessResult(leaderBoard);
        }
        #endregion

        #region Private Methods
        private async Task<ILeaderBoardRecordModel> GetPlayerRecord(IScoreRecordDTO scoreRecord)
        {
            var playerResult = await playerBL.GetPlayerByIdAsync(scoreRecord.PlayerId);

            if (playerResult.Success)
            {
                string name = playerResult.Data?.PlayerName ?? "Unknown Player";
                return new LeaderBoardRecordModel(name, scoreRecord.Score);
            }
            else
            {
                logger.LogError($"Failed to retrieve player with ID {scoreRecord.PlayerId}: {playerResult.Message}");
                return new LeaderBoardRecordModel("Unknown Player", scoreRecord.Score);
            }
        }

        private List<ILeaderBoardRecordModel> SortPlayerRecords(IList<ILeaderBoardRecordModel> recordList, int rankOffset = 0)
        {
            List<ILeaderBoardRecordModel> leaderBoardRecords = [];

            leaderBoardRecords = recordList.ToList().OrderByDescending(elem => elem.Score).ToList();
            leaderBoardRecords = leaderBoardRecords.Select((elem, index) => { elem.Rank = rankOffset + index + 1; return elem; }).ToList();

            return leaderBoardRecords;
        }
        #endregion
    }
}