using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCategoriesController : ControllerBase
    {
        private readonly GameDataContext _context;

        public GameCategoriesController(GameDataContext context)
        {
            _context = context;
        }

        // GET: api/GameCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.GameCategoryDTO>>> GetGameCategories()
        {
            var categories = await _context.GameCategories
                                            .OrderBy(c => c.categoryid) // Sắp xếp theo CategoryId tăng dần
                                           .Select(c => new GameCategoryDTO
                                           {
                                               CategoryId = c.categoryid,
                                               CategoryName = c.categoryname,
                                               Description = c.description
                                           })
                                           .ToListAsync();



            return Ok(categories);
        }

        // GET: api/GameCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameCategoryDTO>> GetGameCategory(int id)
        {
            var category = await _context.GameCategories
                                         .Where(c => c.categoryid == id)
                                         .Select(c => new GameCategoryDTO
                                         {
                                             CategoryId = c.categoryid,
                                             CategoryName = c.categoryname,
                                             Description = c.description
                                         })
                                         .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound($"Gamecategory with ID {id} does not exist.");
            }

            return Ok(category);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByCategory(int categoryId)
        {
            var games = await _context.Games
                .Where(g => g.categoryid == categoryId)
                .OrderBy(g => g.gameid)
                .Select(g => new GameDTO
                {
                    GameId = g.gameid,
                    Title = g.title,
                    Year = g.year,
                    Summary = g.summary,
                    CategoryId = g.categoryid,
                    Price = g.price,
                    ImageURL = g.imageurl
                })
                .ToListAsync();

            if (games == null || !games.Any())
            {
                return NotFound("No games found for the specified category.");
            }

            return Ok(games);
        }


        // POST: api/GameCategories
        [HttpPost]
        public async Task<ActionResult<GameCategoryDTO>> CreateGameCategory(GameCategoryDTO categoryDTO)
        {
            // Truy xuất tất cả ID danh mục hiện có và tìm ID bị thiếu đầu tiên trong bộ nhớ
            var categories = await _context.GameCategories
                                           .OrderBy(c => c.categoryid)
                                           .ToListAsync();

            int newCategoryId = 1; // Start checking from ID 1
            foreach (var category in categories)
            {
                if (category.categoryid == newCategoryId)
                {
                    newCategoryId++;
                }
                else
                {
                    break; //Tìm thấy một khoảng trống trong trình tự
                }
            }

            if (await _context.GameCategories.AnyAsync(m => m.categoryid == newCategoryId))
            {
                return BadRequest($"A game category with ID {newCategoryId} already exists. Adding the game category failed.");
            }

            var categoryToCreate = new GameCategory
            {
                categoryid = newCategoryId,
                categoryname = categoryDTO.CategoryName,
                description = categoryDTO.Description
            };

            _context.GameCategories.Add(categoryToCreate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameCategory), new { id = categoryToCreate.categoryid }, categoryDTO);
        }

        // PUT: api/GameCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameCategory(int id, GameCategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest("ID mismatch. You cannot modify the gamecategory ID.");
            }

            var category = await _context.GameCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"GameCategory with ID {id} does not exist.");
            }

            category.categoryname = categoryDTO.CategoryName;
            category.description = categoryDTO.Description;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok($"Update success ");
        }

        // DELETE: api/GameCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameCategory(int id)
        {
            var category = await _context.GameCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound($"GameCategory with ID {id} does not exist.");
            }

            _context.GameCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok($"Delete success");
        }
    }
}
