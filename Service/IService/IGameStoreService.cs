using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IGameStoreService
    {
        public Task<List<GameModel>> GetAllGameServiceAsync();
        public Task<CreateGameModel> GetAllGenresDropDownListAsync();
        public Task<GenreModel> CreateGenreAsync(GenreModel genreModel);
        public Task<GameModel> CreateGameAsync(GameModel gameModel);
        public Task<EditGameModel> GetGameById(int? gameId);
        public Task<EditGameModel> EditGame(EditGameModel gameId);
        public Task<GameModel> DeleteGame(int? gameId);
    }
}
