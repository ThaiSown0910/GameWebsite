using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddToCartController : ControllerBase
    {
        private readonly GameDataContext _context;

        public AddToCartController(GameDataContext context)
        {
            _context = context;
        }

        // 1. Get all carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetCarts()
        {
            var carts = await _context.Carts
                .Include(c => c.Game)
                .Include(c => c.Login)
                .OrderBy(c => c.cartid)
                .Select(c => new CartDTO
                {
                    CartId = c.cartid,
                    GameId = c.gameid,
                    CustomerId = c.customerid,
                    Quantity = c.quantity,
                    AddedDate = c.addeddate
                })
                .ToListAsync();

            return Ok(carts);
        }

        // 2. Get a cart by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCart(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Game)
                .Include(c => c.Login)
                .Where(c => c.cartid == id)
                .Select(c => new CartDTO
                {
                    CartId = c.cartid,
                    GameId = c.gameid,
                    CustomerId = c.customerid,
                    Quantity = c.quantity,
                    AddedDate = c.addeddate
                })
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // 3. Add a new cart
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCart(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                gameid = cartDTO.GameId,
                customerid = cartDTO.CustomerId,
                quantity = cartDTO.Quantity,
                addeddate = cartDTO.AddedDate
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.cartid }, cartDTO);
        }

        // 4. Update an existing cart
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartDTO cartDTO)
        {
            if (id != cartDTO.CartId)
            {
                return BadRequest("Cart ID mismatch");
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.gameid = cartDTO.GameId;
            cart.customerid = cartDTO.CustomerId;
            cart.quantity = cartDTO.Quantity;
            cart.addeddate = cartDTO.AddedDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // 5. Delete a cart
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cart deleted successfully." });
        }

        // Helper method to check if a cart exists
        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.cartid == id);
        }


        [HttpGet("GetAllCartWithGameAndCustomerDetail")]
        public async Task<ActionResult<IEnumerable<CartWithDetailsDTO>>> GetAllCartWithGameAndCustomerDetail()
        {
            var carts = await _context.Carts
                .Include(c => c.Game) // Load thông tin Game
                .ThenInclude(g => g.GameCategory) // Load thông tin GameCategory nếu cần
                .Include(c => c.Login) // Load thông tin Login (Customer)
                .OrderBy(c => c.cartid)
                .Select(c => new CartWithDetailsDTO
                {
                    CartId = c.cartid,
                    Quantity = c.quantity,
                    AddedDate = c.addeddate,
                    Game = new GameDTO
                    {
                        GameId = c.Game.gameid,
                        Title = c.Game.title,
                        Year = c.Game.year,
                        Summary = c.Game.summary,
                        CategoryId = c.Game.categoryid,
                        Price = c.Game.price,
                        ImageURL = c.Game.imageurl
                    },
                    Customer = new RegisterDTO
                    {
                        CustomerId = c.Login.customerid,
                        UserName = c.Login.username,
                        Password = c.Login.password, // Không nên trả về trong API thực tế, chỉ để minh họa
                        ConfirmPassword = c.Login.password // Không nên trả về trong API thực tế
                    }
                })
                .ToListAsync();

            return Ok(carts);
        }

        // 6. Get all carts for a specific customer with game details
        [HttpGet("GetCartGameByCustomerId/{customerId}")]
        public async Task<ActionResult<IEnumerable<CartWithDetailsDTO>>> GetCartGameByCustomerId(int customerId)
        {
            var carts = await _context.Carts
                .Where(c => c.customerid == customerId) // Filter by customer ID
                .Include(c => c.Game) // Load thông tin Game
                .ThenInclude(g => g.GameCategory) // Load thông tin GameCategory nếu cần
                .Include(c => c.Login) // Load thông tin Login (Customer)
                .OrderBy(c => c.cartid)
                .Select(c => new CartWithDetailsDTO
                {
                    CartId = c.cartid,
                    Quantity = c.quantity,
                    AddedDate = c.addeddate,
                    Game = new GameDTO
                    {
                        GameId = c.Game.gameid,
                        Title = c.Game.title,
                        Year = c.Game.year,
                        Summary = c.Game.summary,
                        CategoryId = c.Game.categoryid,
                        Price = c.Game.price,
                        ImageURL = c.Game.imageurl
                    },
                    Customer = new RegisterDTO
                    {
                        CustomerId = c.Login.customerid,
                        UserName = c.Login.username,
                        //Password = c.Login.password, // Không nên trả về trong API thực tế, chỉ để minh họa
                        //ConfirmPassword = c.Login.password // Không nên trả về trong API thực tế
                    }
                })
                .ToListAsync();

            if (carts == null || !carts.Any())
            {
                return NotFound();
            }

            return Ok(carts);
        }

        [HttpDelete("DeleteCart/{cartId}")]
        public async Task<ActionResult> DeleteCarts(int cartId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.cartid == cartId); // Find the cart by cartId

            if (cart == null)
            {
                return NotFound(); // If no cart found, return NotFound response
            }

            _context.Carts.Remove(cart); // Remove the cart from the DbContext

            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return NoContent (204) to indicate successful deletion without content
        }

    }
}


