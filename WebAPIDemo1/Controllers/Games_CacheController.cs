//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebAPIDemo1.Data;
//using WebAPIDemo1.Model;
//using System.Text.Json;
//using StackExchange.Redis;
//using WebAPIDemo1.Service;

//namespace WebAPIDemo1.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Games_CacheController : ControllerBase
//    {
//        private readonly GameDataContext _context;
//        private readonly IRedisCacheService _cacheService;

//        public Games_CacheController(GameDataContext context, IRedisCacheService cacheService)
//        {
//            _context = context;
//            _cacheService = cacheService;
//        }

//        // GET: api/Games
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
//        {
//            const string cacheKey = "games";

//            // Kiểm tra cache trước khi truy vấn dữ liệu
//            var cachedGames = await _cacheService.GetCacheAsync(cacheKey);
//            if (cachedGames != null)
//            {
//                return Ok(JsonSerializer.Deserialize<IEnumerable<GameDTO>>(cachedGames));
//            }

//            var games = await _context.Games
//                                       .OrderBy(m => m.gameid)
//                                       .Select(m => new GameDTO
//                                       {
//                                           GameId = m.gameid,
//                                           Title = m.title,
//                                           Year = m.year,
//                                           Summary = m.summary,
//                                           CategoryId = m.categoryid,
//                                           Price = m.price,
//                                           ImageURL = m.imageurl,
//                                       })
//                                       .ToListAsync();

//            // Cache kết quả sau khi truy vấn
//            await _cacheService.SetCacheAsync(cacheKey, JsonSerializer.Serialize(games), TimeSpan.FromSeconds(60));
//            return Ok(games);
//        }

//        // GET: api/Games/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<GameDTO>> GetGames(int id)
//        {
//            string cacheKey = $"game_{id}";

//            // Kiểm tra cache trước khi truy vấn dữ liệu
//            var cachedGame = await _cacheService.GetCacheAsync(cacheKey);
//            if (cachedGame != null)
//            {
//                return Ok(JsonSerializer.Deserialize<GameDTO>(cachedGame));
//            }

//            var game = await _context.Games
//                                      .Where(m => m.gameid == id)
//                                      .Select(m => new GameDTO
//                                      {
//                                          GameId = m.gameid,
//                                          Title = m.title,
//                                          Year = m.year,
//                                          Summary = m.summary,
//                                          CategoryId = m.categoryid,
//                                          Price = m.price,
//                                          ImageURL = m.imageurl
//                                      })
//                                      .FirstOrDefaultAsync();

//            if (game == null)
//            {
//                return NotFound($"Game with ID {id} does not exist.");
//            }

//            // Cache kết quả sau khi truy vấn
//            await _cacheService.SetCacheAsync(cacheKey, JsonSerializer.Serialize(game), TimeSpan.FromSeconds(120));
//            return Ok(game);
//        }

//        // POST: api/Games
//        [HttpPost]
//        public async Task<ActionResult<GameDTO>> CreateGame(GameDTO gameDTO)
//        {
//            var games = await _context.Games.OrderBy(m => m.gameid).ToListAsync();

//            int newGameId = 1;
//            foreach (var game in games)
//            {
//                if (game.gameid == newGameId)
//                {
//                    newGameId++;
//                }
//                else
//                {
//                    break;
//                }
//            }

//            if (await _context.Games.AnyAsync(m => m.gameid == newGameId))
//            {
//                return BadRequest($"A game with ID {newGameId} already exists. Adding the game failed.");
//            }

//            var gameToCreate = new Game
//            {
//                gameid = newGameId,
//                title = gameDTO.Title,
//                year = gameDTO.Year,
//                summary = gameDTO.Summary,
//                categoryid = gameDTO.CategoryId,
//                price = gameDTO.Price,
//                imageurl = gameDTO.ImageURL
//            };

//            _context.Games.Add(gameToCreate);
//            await _context.SaveChangesAsync();

//            // Xóa cache để dữ liệu được cập nhật khi có thêm game mới
//            await _cacheService.SetCacheAsync("games", null);

//            return CreatedAtAction(nameof(GetGames), new { id = gameToCreate.gameid }, gameDTO);
//        }

//        // PUT: api/Games/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateGame(int id, GameDTO gameDTO)
//        {
//            if (id != gameDTO.GameId)
//            {
//                return BadRequest("ID mismatch. You cannot modify the game ID.");
//            }

//            var game = await _context.Games.FindAsync(id);
//            if (game == null)
//            {
//                return NotFound($"Game with ID {id} does not exist.");
//            }

//            game.title = gameDTO.Title;
//            game.year = gameDTO.Year;
//            game.summary = gameDTO.Summary;
//            game.categoryid = gameDTO.CategoryId;
//            game.price = gameDTO.Price;
//            game.imageurl = gameDTO.ImageURL;

//            _context.Entry(game).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            // Xóa cache để dữ liệu cập nhật sau khi sửa đổi
//            await _cacheService.SetCacheAsync("games", null);
//            await _cacheService.SetCacheAsync($"game_{id}", null);

//            return Ok("Update success");
//        }

//        // DELETE: api/Games/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteGame(int id)
//        {
//            var game = await _context.Games.FindAsync(id);
//            if (game == null)
//            {
//                return NotFound($"Game with ID {id} does not exist.");
//            }

//            _context.Games.Remove(game);
//            await _context.SaveChangesAsync();

//            // Xóa cache sau khi game bị xóa
//            await _cacheService.SetCacheAsync("games", null);
//            await _cacheService.SetCacheAsync($"game_{id}", null);

//            return Ok("Delete success");
//        }
//    }
//}
