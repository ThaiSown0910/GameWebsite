using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly GameDataContext _context;

        public CheckoutController(GameDataContext context)
        {
            _context = context;
        }

        // GET: api/Checkout
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckoutDTO>>> GetCheckouts()
        {
            var checkouts = await _context.Checkouts
                .Include(c => c.Login)  // Optional: Include related Login if needed
                .Select(c => new CheckoutDTO
                {
                    SaleId = c.saleid,
                    CustomerId = c.customerid,
                    SaleDate = c.saledate,
                    TotalInvoiceAmount = c.totalinvoiceamount,
                    Discount = c.discount,
                    PaymentNaration = c.paymentnaration,
                    DeliveryAddress1 = c.deliveryaddress1,
                    DeliveryAddress2 = c.deliveryaddress2,
                    DeliveryCity = c.deliverycity,
                    DeliveryPinCode = c.deliverypincode,
                    DeliveryLandMark = c.deliverylandmark
                })
                .ToListAsync();

            return Ok(checkouts);
        }

        // GET: api/Checkout/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutDTO>> GetCheckout(int id)
        {
            var checkout = await _context.Checkouts
                .Where(c => c.saleid == id)
                .Include(c => c.Login)  // Optional: Include related Login if needed
                .FirstOrDefaultAsync();

            if (checkout == null)
            {
                return NotFound();
            }

            var checkoutDTO = new CheckoutDTO
            {
                SaleId = checkout.saleid,
                CustomerId = checkout.customerid,
                SaleDate = checkout.saledate,
                TotalInvoiceAmount = checkout.totalinvoiceamount,
                Discount = checkout.discount,
                PaymentNaration = checkout.paymentnaration,
                DeliveryAddress1 = checkout.deliveryaddress1,
                DeliveryAddress2 = checkout.deliveryaddress2,
                DeliveryCity = checkout.deliverycity,
                DeliveryPinCode = checkout.deliverypincode,
                DeliveryLandMark = checkout.deliverylandmark
            };

            return Ok(checkoutDTO);
        }

        // POST: api/Checkout
        [HttpPost]
        public async Task<ActionResult<CheckoutDTO>> PostCheckout(CheckoutDTO checkoutDTO)
        {
            var checkout = new Checkout
            {
                customerid = checkoutDTO.CustomerId,
                saledate = checkoutDTO.SaleDate,
                totalinvoiceamount = checkoutDTO.TotalInvoiceAmount,
                discount = checkoutDTO.Discount,
                paymentnaration = checkoutDTO.PaymentNaration,
                deliveryaddress1 = checkoutDTO.DeliveryAddress1,
                deliveryaddress2 = checkoutDTO.DeliveryAddress2,
                deliverycity = checkoutDTO.DeliveryCity,
                deliverypincode = checkoutDTO.DeliveryPinCode,
                deliverylandmark = checkoutDTO.DeliveryLandMark
            };

            _context.Checkouts.Add(checkout);
            await _context.SaveChangesAsync();

            // Return the created object
            checkoutDTO.SaleId = checkout.saleid;

            return CreatedAtAction("GetCheckout", new { id = checkout.saleid }, checkoutDTO);
        }   

        // PUT: api/Checkout/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckout(int id, CheckoutDTO checkoutDTO)
        {
            if (id != checkoutDTO.SaleId)
            {
                return BadRequest();
            }

            var checkout = await _context.Checkouts.FindAsync(id);

            if (checkout == null)
            {
                return NotFound();
            }

            // Update fields
            checkout.customerid = checkoutDTO.CustomerId;
            checkout.saledate = checkoutDTO.SaleDate;
            checkout.totalinvoiceamount = checkoutDTO.TotalInvoiceAmount;
            checkout.discount = checkoutDTO.Discount;
            checkout.paymentnaration = checkoutDTO.PaymentNaration;
            checkout.deliveryaddress1 = checkoutDTO.DeliveryAddress1;
            checkout.deliveryaddress2 = checkoutDTO.DeliveryAddress2;
            checkout.deliverycity = checkoutDTO.DeliveryCity;
            checkout.deliverypincode = checkoutDTO.DeliveryPinCode;
            checkout.deliverylandmark = checkoutDTO.DeliveryLandMark;

            _context.Entry(checkout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Checkout/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckout(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }

            _context.Checkouts.Remove(checkout);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.saleid == id);
        }
    }
}
