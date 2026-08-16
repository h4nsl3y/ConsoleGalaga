using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.PlayerModel;
using BusinessLayer.Models.ServiceResponse;
using DataLayer.DAL.PlayerDAL;
using DataLayer.DTO.PlayerDTO;
using Helpers.LoggerHelper;

namespace BusinessLayer.BL.PlayerBL
{
    public class PlayerBL(IPlayerDAL playerDAL, ILogger logger) : IPlayerBL
    {
        #region Public Methods
        public async Task<IServiceResponse<int>> AddPlayerAsync(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
                return ServiceResponse<int>.FailureResult("Player name cannot be empty.");

            var response = await playerDAL.AddPlayerAsync(playerName);

            if (!response.Success)
            {
                string errorMessage = "Failed to add player";
                logger.LogError($"{errorMessage} : {response.Message}");
                return ServiceResponse<int>.FailureResult(errorMessage);
            }
            var playerIdResponse = await playerDAL.GetPlayerByNameAsync(playerName);

            if (!playerIdResponse.Success || playerIdResponse.Data == null)
            {
                string errorMessage = "Failed to retrieve player ID";
                logger.LogError($"{errorMessage} for player {playerName}: {playerIdResponse.Message}");
                return ServiceResponse<int>.FailureResult(errorMessage);
            }

            int playerId = playerIdResponse.Data.PlayerId;
            return ServiceResponse<int>.SuccessResult(playerId);
        }

        public async Task<IServiceResponse<bool>> DeletePlayerByIdAsync(int playerId)
        {
            var response = await playerDAL.DeletePlayerByIdAsync(playerId);

            if (!response.Success)
            {
                string errorMessage = "Failed to delete player";
                logger.LogError($"{errorMessage} with ID {playerId}: {response.Message}");
                return ServiceResponse<bool>.FailureResult(errorMessage);
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<IPlayerModel>> GetPlayerByIdAsync(int playerId)
        {
            var response = await playerDAL.GetPlayerByIdAsync(playerId);

            if (!response.Success || response.Data == null)
            {
                string errorMessage = "Failed to retrieve player";
                logger.LogError($"{errorMessage} with ID {playerId}: {response.Message}");
                return ServiceResponse<IPlayerModel>.FailureResult(errorMessage);
            }
            var playerModel = new PlayerModel(response.Data.PlayerId, response.Data.PlayerName, response.Data.DateCreated);
            return ServiceResponse<IPlayerModel>.SuccessResult(playerModel);
        }

        public async Task<IGridModel<IPlayerModel>> GetAllPlayersAsync(int page = 0, int limit = 10)
        {
            limit = (limit <= 0) ? 10 : limit;
            page = (page < 0) ? 0 : page;

            var countResponse = await playerDAL.GetAllPlayersCountAsync();
            int totalCount = (countResponse.Success) ? countResponse.Data : 0;

            var response = await playerDAL.GetAllPlayersAsync(page, limit);
            var gridTitles = new List<string> { "PlayerId", "PlayerName", "DateCreated" };

            if (!response.Success || response.Data == null)
            {
                logger.LogError($"Failed to retrieve all players: {response.Message}");
                return new GridModel<IPlayerModel>(gridTitles, [], page, 0, limit);
            }
            var playerModels = response.Data.Select(p => (IPlayerModel)new PlayerModel(p.PlayerId, p.PlayerName, p.DateCreated)).ToList();
            return new GridModel<IPlayerModel>(gridTitles, playerModels, page, totalCount, limit);
        }

        public async Task<IServiceResponse<bool>> UpdatePlayerAsync(IPlayerModel player)
        {
            var dalPlayer = new PlayerDTO(player.PlayerId, player.PlayerName, player.DateCreated);
            var response = await playerDAL.UpdatePlayerAsync(dalPlayer);

            if (!response.Success)
            {
                string errorMessage = "Failed to update player";
                logger.LogError($"{errorMessage} with ID {player.PlayerId}: {response.Message}");
                return ServiceResponse<bool>.FailureResult(errorMessage);
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }
        #endregion

        #region Private Methods
        private static IList<IPlayerDTO> ConvertToDTO(IList<IPlayerModel> model)
        {
            return model
                .Select(static m => (IPlayerDTO)new PlayerDTO(m.PlayerId, m.PlayerName))
                .ToList();
        }

        private static IList<IPlayerModel> ConvertToModel(IList<IPlayerDTO> dto)
        {
            return dto
                .Select(static d => (IPlayerModel)new PlayerModel(d.PlayerId, d.PlayerName))
                .ToList();
        }
        #endregion
    }
}
