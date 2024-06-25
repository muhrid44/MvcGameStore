using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IGameStoreRepository
    {
        public Task<List<GameModel>> GetAllGameRepositoryAsync();
        public Task<CreateGameModel> GetAllGenresDropDownListAsync();
        public Task<GenreModel> CreateGenreAsync(GenreModel genreModel);
        public Task<GameModel> CreateGameAsync(GameModel gameModel);
        public Task<EditGameModel> GetGameById(int? gameId);
        public Task<EditGameModel> EditGame(EditGameModel editGameModel);
        public Task<GameModel> DeleteGame(int? gameId);
    }
}
