using Data;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class GameStoreService : IGameStoreService
    {
        private readonly IGameStoreRepository _gameStoreRepository;

        public GameStoreService(IGameStoreRepository gameStoreRepository)
        {
            _gameStoreRepository = gameStoreRepository;
        }

        public async Task<GameModel> CreateGameAsync(GameModel gameModel)
        {
            var result = await _gameStoreRepository.CreateGameAsync(gameModel);
            return result;
        }

        public async Task<GenreModel> CreateGenreAsync(GenreModel genreModel)
        {
            var result = await _gameStoreRepository.CreateGenreAsync(genreModel);
            return result;
        }

        public async Task<GameModel> DeleteGame(int? gameId)
        {
            var gameModel = await _gameStoreRepository.DeleteGame(gameId);
            return gameModel;
        }

        public async Task<EditGameModel> EditGame(EditGameModel editGameModel)
        {
            var gameModel = await _gameStoreRepository.EditGame(editGameModel);
            return gameModel;
        }

        public async Task<List<GameModel>> GetAllGameServiceAsync()
        {
            List<GameModel> result = await _gameStoreRepository.GetAllGameRepositoryAsync();
            return result;
        }

        public async Task<CreateGameModel> GetAllGenresDropDownListAsync()
        {
            CreateGameModel result = await _gameStoreRepository.GetAllGenresDropDownListAsync();
            return result;
        }

        public async Task<EditGameModel> GetGameById(int? gameId)
        {
            EditGameModel gameModelResult = await _gameStoreRepository.GetGameById(gameId);
            return gameModelResult;
        }
    }
}
