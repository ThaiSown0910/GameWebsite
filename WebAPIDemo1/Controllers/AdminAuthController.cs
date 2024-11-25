using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;

namespace WebAPIDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly GameDataContext _context;

        public AdminAuthController(GameDataContext context)
        {
            _context = context;
        }

        // GET: api/AdminAuth
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminCreateDTO>>> GetAdminCreates()
        {
            var adminCreates = await _context.AdminCreates
                .OrderBy(a => a.adminid)
                .Select(a => new AdminCreateDTO
                {
                    AdminID = a.adminid,
                    UserName = a.username,
                    FullName = a.fullname,
                    Password = a.password,
                    ConfirmPassword = a.confirmpassword,
                    MobilePhone = a.mobilephone
                })
                .ToListAsync();

            return Ok(adminCreates);
        }

        // GET: api/AdminAuth/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminCreateDTO>> GetAdminCreate(int id)
        {
            var adminCreate = await _context.AdminCreates
                .Where(a => a.adminid == id)
                .Select(a => new AdminCreateDTO
                {
                    AdminID = a.adminid,
                    UserName = a.username,
                    FullName = a.fullname,
                    Password = a.password,
                    ConfirmPassword = a.confirmpassword,
                    MobilePhone = a.mobilephone
                })
                .FirstOrDefaultAsync();

            if (adminCreate == null)
            {
                return NotFound();
            }

            return Ok(adminCreate);
        }

        // POST: api/AdminAuth
        [HttpPost]
        public async Task<ActionResult<AdminCreateDTO>> CreateAdmin(AdminCreateDTO adminCreateDTO)
        {
            if (adminCreateDTO == null)
            {
                return BadRequest();
            }

            // Find the nearest available ID for adminid
            var nearestAvailableId = await FindNearestAvailableId();

            // Create AdminCreate entity
            var adminCreate = new AdminCreate
            {
                adminid = nearestAvailableId,  // Manually assign the nearest available ID
                username = adminCreateDTO.UserName,
                password = adminCreateDTO.Password,  // In real scenario, hash the password
                confirmpassword = adminCreateDTO.ConfirmPassword,
                fullname = adminCreateDTO.FullName,
                mobilephone = adminCreateDTO.MobilePhone
            };

            _context.AdminCreates.Add(adminCreate);
            await _context.SaveChangesAsync();

            // Create AdminLogin entity and associate with AdminCreate
            var adminLogin = new AdminLogin
            {
                adminid = adminCreate.adminid,
                username = adminCreate.username,
                password = adminCreate.password  // In real scenario, hash the password
            };

            _context.AdminLogins.Add(adminLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdminCreate), new { id = adminCreate.adminid }, adminCreateDTO);
        }

        // Method to find the nearest available ID
        private async Task<int> FindNearestAvailableId()
        {
            var allIds = await _context.AdminCreates.Select(a => a.adminid).OrderBy(id => id).ToListAsync();
            int nearestAvailableId = 1;

            foreach (var id in allIds)
            {
                if (id != nearestAvailableId)
                {
                    break;
                }
                nearestAvailableId++;
            }

            return nearestAvailableId;
        }


        // PUT: api/AdminAuth/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, AdminCreateDTO adminCreateDTO)
        {
            var adminCreate = await _context.AdminCreates.FindAsync(id);
            if (adminCreate == null)
            {
                return NotFound();
            }

            // Update AdminCreate entity
            adminCreate.username = adminCreateDTO.UserName;
            adminCreate.password = adminCreateDTO.Password;  // Ensure password is hashed
            adminCreate.confirmpassword = adminCreateDTO.ConfirmPassword;
            adminCreate.fullname = adminCreateDTO.FullName;
            adminCreate.mobilephone = adminCreateDTO.MobilePhone;

            _context.Entry(adminCreate).State = EntityState.Modified;

            // Update AdminLogin entity as well
            var adminLogin = await _context.AdminLogins
                .FirstOrDefaultAsync(a => a.adminid == id);

            if (adminLogin != null)
            {
                adminLogin.username = adminCreateDTO.UserName;
                adminLogin.password = adminCreateDTO.Password; // Ensure password is hashed
                _context.Entry(adminLogin).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/AdminAuth/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var adminCreate = await _context.AdminCreates.FindAsync(id);
            if (adminCreate == null)
            {
                return NotFound();
            }

            var adminLogin = await _context.AdminLogins
                .FirstOrDefaultAsync(a => a.adminid == id);

            // Remove AdminLogin if exists
            if (adminLogin != null)
            {
                _context.AdminLogins.Remove(adminLogin);
            }

            // Remove AdminCreate
            _context.AdminCreates.Remove(adminCreate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/AdminAuth/Login
        [HttpGet("Login")]
        public async Task<ActionResult<IEnumerable<AdminLoginDTO>>> GetAdminLogins()
        {
            var adminLogins = await _context.AdminLogins
                .Select(a => new AdminLoginDTO
                {
                    UserName = a.username,
                    Password = a.password
                })
                .ToListAsync();

            return Ok(adminLogins);
        }

        // GET: api/AdminAuth/Login/{id}
        [HttpGet("Login/{id}")]
        public async Task<ActionResult<AdminLoginDTO>> GetAdminLogin(int id)
        {
            var adminLogin = await _context.AdminLogins
                .Where(a => a.adminid == id)
                .Select(a => new AdminLoginDTO
                {
                    UserName = a.username,
                    Password = a.password
                })
                .FirstOrDefaultAsync();

            if (adminLogin == null)
            {
                return NotFound();
            }

            return Ok(adminLogin);
        }

        // POST: api/AdminAuth/Login
        [HttpPost("Login")]
        public async Task<ActionResult<object>> LoginAdmin(AdminLoginDTO adminLoginDTO)
        {
            if (adminLoginDTO == null)
            {
                return BadRequest();
            }

            var adminLogin = await _context.AdminLogins
                .FirstOrDefaultAsync(a => a.username == adminLoginDTO.UserName && a.password == adminLoginDTO.Password);

            if (adminLogin == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Return success message along with login information
            return Ok(new
            {
                Message = "Login successful",
                LoginInfo = new AdminLoginDTO
                {
                    UserName = adminLogin.username,
                    Password = adminLogin.password
                }
            });
        }

    }

}
