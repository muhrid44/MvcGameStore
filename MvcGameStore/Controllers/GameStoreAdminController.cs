using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Service.IService;
using Microsoft.Extensions.Caching.Memory;

namespace MvcGameStore.Controllers
{
    public class GameStoreAdminController : Controller
    {
        private readonly MvcGameStoreContext _context;
        private readonly IGameStoreService _gameStoreService;
        private IMemoryCache _cache;


        public GameStoreAdminController(MvcGameStoreContext context, IGameStoreService gameStoreService, IMemoryCache cache)
        {
            _context = context;
            _gameStoreService = gameStoreService;
            _cache = cache;
        }

        // GET: GameModels
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = new List<GameModel>();
                if(_cache.TryGetValue("HomeCache", out result))
                {
                    //retrieving data from cache
                } else
                {
                    result = await _gameStoreService.GetAllGameServiceAsync();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                    _cache.Set("HomeCache", result, cacheEntryOptions);
                }
                return View(result);
            }
            catch (Exception ex) {
                return View("Views/Shared/Error.cshtml", ex.Message);
            }
        }

        // GET: GameModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _gameStoreService.GetGameById(id);
            if (gameModel.GameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // GET: GameModels/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                var result = await _gameStoreService.GetAllGenresDropDownListAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                return View("Views/Shared/Error.cshtml", ex.Message);
            }
        }

        public IActionResult CreateGenre()
        {
            return View();
        }

        // POST: GameModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateGameModel createGameModel)
        {
            try
            {
                if (createGameModel != null)
                {
                    var result = await _gameStoreService.CreateGameAsync(createGameModel.GameModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(createGameModel.GameModel);
            }
            catch (Exception ex)
            {
                return View("Views/Shared/Error.cshtml", ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre([Bind("Id,GenreName,AdultOnly")] GenreModel genreModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _gameStoreService.CreateGenreAsync(genreModel);
                    return RedirectToAction(nameof(Index));
                }
                return View(genreModel);
            }
            catch (Exception ex)
            {
                return View("Views/Shared/Error.cshtml", ex.InnerException);
            }
        }

        // GET: GameModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _gameStoreService.GetGameById(id);
            if (gameModel == null)
            {
                return NotFound();
            }
            return View(gameModel);
        }

        // POST: GameModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditGameModel editGameModel)
        {
            if (editGameModel != null)
            {
                try
                {
                    await _gameStoreService.EditGame(editGameModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameModelExists(editGameModel.GameModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editGameModel);
        }

        // GET: GameModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // POST: GameModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id != null)
                {
                    var result = await _gameStoreService.DeleteGame(id);
                    return RedirectToAction(nameof(Index));
                } else
                {
                return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                return View("Views/Shared/Error.cshtml", ex.Message);
            }
        }

        private bool GameModelExists(int id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
