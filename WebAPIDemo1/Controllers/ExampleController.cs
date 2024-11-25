using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo1.Service;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IRedisCacheService _cacheService;

        public ExampleController(IRedisCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("get-cache")]
        public async Task<IActionResult> GetCache(string key)
        {
            var value = await _cacheService.GetCacheAsync(key);
            return Ok(value ?? "No cache value found");
        }

        [HttpPost("set-cache")]
        public async Task<IActionResult> SetCache(string key, string value)
        {
            await _cacheService.SetCacheAsync(key, value, TimeSpan.FromSeconds(30)); // Cài đặt hết hạn sau 5 phút
            return Ok("Cache value set");
        }
    }
}
