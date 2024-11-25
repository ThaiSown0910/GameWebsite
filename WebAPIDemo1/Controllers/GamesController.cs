using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;
using WebAPIDemo1.Service;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameDataContext _context;
        private readonly IRedisCacheService _cacheService;

        public GamesController(GameDataContext context, IRedisCacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            const string cacheKey = "games";

            // Kiểm tra cache trước khi truy vấn dữ liệu
            var cachedGames = await _cacheService.GetCacheAsync(cacheKey);
            if (cachedGames != null)
            {
                return Ok(JsonSerializer.Deserialize<IEnumerable<GameDTO>>(cachedGames));
            }

            var games = await _context.Games
                                       .OrderBy(m => m.gameid)
                                       .Select(m => new GameDTO
                                       {
                                           GameId = m.gameid,
                                           Title = m.title,
                                           Year = m.year,
                                           Summary = m.summary,
                                           CategoryId = m.categoryid,
                                           Price = m.price,
                                           ImageURL = m.imageurl
                                       })
                                       .ToListAsync();

            // Cache kết quả sau khi truy vấn
            await _cacheService.SetCacheAsync(cacheKey, JsonSerializer.Serialize(games), TimeSpan.FromSeconds(120));
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGameById(int id)
        {
            string cacheKey = $"game_{id}";

            // Kiểm tra cache trước khi truy vấn dữ liệu
            var cachedGame = await _cacheService.GetCacheAsync(cacheKey);
            if (cachedGame != null)
            {
                return Ok(JsonSerializer.Deserialize<GameDTO>(cachedGame));
            }

            var game = await _context.Games
                                      .Where(m => m.gameid == id)
                                      .Select(m => new GameDTO
                                      {
                                          GameId = m.gameid,
                                          Title = m.title,
                                          Year = m.year,
                                          Summary = m.summary,
                                          CategoryId = m.categoryid,
                                          Price = m.price,
                                          ImageURL = m.imageurl
                                      })
                                      .FirstOrDefaultAsync();

            if (game == null)
            {
                return NotFound($"Game with ID {id} does not exist.");
            }

            // Cache kết quả sau khi truy vấn
            await _cacheService.SetCacheAsync(cacheKey, JsonSerializer.Serialize(game), TimeSpan.FromSeconds(120));
            return Ok(game);
        }


        // GET: api/Games/search?title=gameTitle
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> SearchGamesByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title parameter is required.");
            }

            // Use a regular variable instead of constant
            string cacheKey = $"games_search_{title.ToLower()}";

            // Check the cache before querying the database
            var cachedGames = await _cacheService.GetCacheAsync(cacheKey);
            if (cachedGames != null)
            {
                return Ok(JsonSerializer.Deserialize<IEnumerable<GameDTO>>(cachedGames));
            }
            // Use ToLower() to ensure case-insensitive search
            var games = await _context.Games
                                       .Where(m => m.title.ToLower().Contains(title.ToLower()))
                                       .OrderBy(m => m.gameid)
                                       .Select(m => new GameDTO
                                       {
                                           GameId = m.gameid,
                                           Title = m.title,
                                           Year = m.year,
                                           Summary = m.summary,
                                           CategoryId = m.categoryid,
                                           Price = m.price,
                                           ImageURL = m.imageurl
                                       })
                                       .ToListAsync();


            if (games == null || !games.Any())
            {
                return NotFound($"No games found with title containing '{title}'.");
            }

            // Cache the result after querying the database
            await _cacheService.SetCacheAsync(cacheKey, JsonSerializer.Serialize(games), TimeSpan.FromSeconds(120));

            return Ok(games);
        }



        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<GameDTO>> CreateGame(GameDTO gameDTO)
        {
            if (gameDTO == null)
            {
                return BadRequest("Invalid game data.");
            }

            // Kiểm tra xem gameid có tồn tại hay không
            var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.gameid == gameDTO.GameId);
            if (existingGame != null)
            {
                return Conflict($"Game with ID {gameDTO.GameId} already exists.");
            }

            // Nếu không có gameid trùng, tạo gameid mới liền kề
            int newGameId = 0;
            var lastGame = await _context.Games.OrderByDescending(g => g.gameid).FirstOrDefaultAsync();
            if (lastGame != null)
            {
                newGameId = lastGame.gameid + 1;  // Tạo gameid mới liền kề
            }
            else
            {
                newGameId = 1;  // Nếu là game đầu tiên, gameid bắt đầu từ 1
            }

           

            // Tạo game mới với gameid tự động
            var gameToCreate = new Game
            {
                gameid = newGameId,  // Gán gameid mới
                title = gameDTO.Title,
                year = gameDTO.Year,
                summary = gameDTO.Summary,
                categoryid = gameDTO.CategoryId,
                price = gameDTO.Price,
                imageurl = gameDTO.ImageURL

            };

            _context.Games.Add(gameToCreate);
            await _context.SaveChangesAsync();

            // Xóa cache để dữ liệu được cập nhật
            await _cacheService.SetCacheAsync("games", null);

            // Trả về imageBase64 cùng với thông tin game
           
            return CreatedAtAction(nameof(GetGameById), new { id = gameToCreate.gameid }, gameDTO);
        }



        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, GameDTO gameDTO)
        {
            if (id != gameDTO.GameId)
            {
                return BadRequest("ID mismatch. You cannot modify the game ID.");
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} does not exist.");
            }

            // Cập nhật thông tin game
            game.title = gameDTO.Title;
            game.year = gameDTO.Year;
            game.summary = gameDTO.Summary;
            game.categoryid = gameDTO.CategoryId;
            game.price = gameDTO.Price;
            game.imageurl = gameDTO.ImageURL;

            // Cập nhật ảnh nếu có
           
            

            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Xóa cache để dữ liệu được cập nhật
            await _cacheService.SetCacheAsync("games", null);
            await _cacheService.SetCacheAsync($"game_{id}", null);

            return Ok("Update success");
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} does not exist.");
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            // Xóa cache sau khi game bị xóa
            await _cacheService.SetCacheAsync("games", null);
            await _cacheService.SetCacheAsync($"game_{id}", null);

            return Ok("Delete success");
        }
    }
}

