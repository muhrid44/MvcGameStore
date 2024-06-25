using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private readonly MvcGameStoreContext _context;
        private IMemoryCache _cache;

        public GameStoreRepository(MvcGameStoreContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<GameModel> CreateGameAsync(GameModel gameModel)
        {
            _context.Add(gameModel);
            await _context.SaveChangesAsync();
            _cache.Remove("HomeCache");
            return gameModel;
        }

        public async Task<GenreModel> CreateGenreAsync(GenreModel genreModel)
        {
            _context.Add(genreModel);
            await _context.SaveChangesAsync();
            return genreModel;
        }

        public async Task<GameModel> DeleteGame(int? id)
        {
            var gameModel = await _context.Game.FindAsync(id);
            _context.Game.Remove(gameModel);
            await _context.SaveChangesAsync();
            _cache.Remove("HomeCache");
            return gameModel;
        }

        public async Task<EditGameModel> EditGame(EditGameModel editGameModel)
        {
            var gameModel = await _context.Game.FindAsync(editGameModel.GameModel.Id);
            gameModel.Title = editGameModel.GameModel.Title;
            gameModel.ReleaseDate = editGameModel.GameModel.ReleaseDate;
            gameModel.Price = editGameModel.GameModel.Price; 
            gameModel.GenreId = editGameModel.GameModel.GenreId;

            await _context.SaveChangesAsync();
            _cache.Remove("HomeCache");

            return editGameModel;
        }

        public async Task<List<GameModel>> GetAllGameRepositoryAsync()
        {
            List<GameModel> gameModelResult = await _context.Game.ToListAsync();
            return gameModelResult;
        }

        public async Task<CreateGameModel> GetAllGenresDropDownListAsync()
        {
            List<GenreModel> genres = await _context.Genre.ToListAsync();
            CreateGameModel createGameModels = new CreateGameModel();
            createGameModels.Genres = genres;

            foreach (var genre in genres) {
                if (genre.AdultOnly)
                {
                    genre.GenreName += " - Adults Only";
                }
            }

            return createGameModels;
        }

        public async Task<EditGameModel> GetGameById(int? gameId)
        {
            EditGameModel editGameModel = new EditGameModel();
            GameModel gameModelResult = await _context.Game.FirstOrDefaultAsync(data => data.Id == gameId);

            editGameModel.GameModel = gameModelResult;

            if(gameModelResult != null)
            {
                string txt = editGameModel.GameModel.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);
                //back to a double
                double priceFixing = double.Parse(txt, System.Globalization.CultureInfo.InvariantCulture);
                editGameModel.GameModel.Price = Convert.ToDecimal(priceFixing);
            }

            var listGenre = await GetAllGenresDropDownListAsync();

            editGameModel.Genres = listGenre.Genres;

           return editGameModel;
        }
    }
}
