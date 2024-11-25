using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIDemo1.Data;
using WebAPIDemo1.Model;

namespace WebAPIDemo1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly GameDataContext _context;

        public RegisterController(GameDataContext context)
        {
            _context = context;
        }

        // GET: api/register
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterDTO>>> GetRegisters()
        {
            return await _context.Registers
                .OrderBy(r => r.customerid) // Sort by customerid in ascending order
                .Select(r => new RegisterDTO
                {
                    CustomerId = r.customerid,
                    UserName = r.username,
                    Password = r.password,
                    ConfirmPassword = r.confirmpassword,
                    Email = r.email
                })
                .ToListAsync();
        }


        // GET: api/register/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterDTO>> GetRegister(int id)
        {
            var register = await _context.Registers.FindAsync(id);

            if (register == null)
            {
                return NotFound();
            }

            return new RegisterDTO
            {
                CustomerId = register.customerid,
                UserName = register.username,
                Password = register.password,
                ConfirmPassword = register.confirmpassword,
                Email = register.email
            };
        }

        // POST: api/register
        [HttpPost]
        public async Task<ActionResult<RegisterDTO>> CreateRegister(RegisterDTO registerDTO)
        {
            // Kiểm tra nếu username đã tồn tại
            if (await _context.Registers.AnyAsync(r => r.username == registerDTO.UserName))
            {
                return BadRequest("Tài khoản đã tồn tại.");
            }

            // Kiểm tra nếu email đã tồn tại
            if (await _context.Registers.AnyAsync(r => r.email == registerDTO.Email))
            {
                return BadRequest("Email đã được sử dụng.");
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(registerDTO.Email))
            {
                return BadRequest("Email không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: example@domain.com).");
            }

            // Kiểm tra nếu password và confirmpassword khớp
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Mật khẩu và xác nhận mật khẩu không khớp.");
            }

            // Tìm ID nhỏ nhất chưa sử dụng
            int newCustomerId = 1; // Bắt đầu từ 1
            var allIds = await _context.Registers.OrderBy(r => r.customerid).Select(r => r.customerid).ToListAsync();

            foreach (var id in allIds)
            {
                if (id == newCustomerId)
                {
                    newCustomerId++; // Nếu ID này đã tồn tại, tăng lên 1
                }
                else
                {
                    break; // Nếu gặp khoảng trống, dừng lại
                }
            }

            // Tạo tài khoản mới với customerid đã được xác định
            var register = new Register
            {
                customerid = newCustomerId,
                username = registerDTO.UserName,
                password = registerDTO.Password,
                confirmpassword = registerDTO.ConfirmPassword,
                email = registerDTO.Email // Thêm email vào dữ liệu
            };

            _context.Registers.Add(register);
            await _context.SaveChangesAsync();

            // Tự động thêm dữ liệu vào bảng Login sau khi đăng ký
            var login = new Login
            {
                customerid = register.customerid,
                username = register.username,
                password = register.password
            };
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            // Cập nhật lại registerDTO với customerid mới
            registerDTO.CustomerId = register.customerid;

            // Trả về phản hồi với thông báo thành công
            return CreatedAtAction(nameof(GetRegister), new { id = register.customerid }, new
            {
                Message = "Đã tạo tài khoản thành công",
                Data = registerDTO
            });
        }

        // Hàm kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }



        // PUT: api/register/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegister(int id, RegisterDTO registerDTO)
        {
            if (id != registerDTO.CustomerId)
            {
                return BadRequest("Customer ID không khớp.");
            }

            var register = await _context.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound("Tài khoản không tồn tại.");
            }

            // Kiểm tra nếu username mới đã tồn tại trên tài khoản khác
            if (register.username != registerDTO.UserName &&
                await _context.Registers.AnyAsync(r => r.username == registerDTO.UserName))
            {
                return BadRequest("Tên tài khoản đã được sử dụng bởi người dùng khác.");
            }

            // Kiểm tra nếu email mới đã tồn tại trên tài khoản khác
            if (register.email != registerDTO.Email &&
                await _context.Registers.AnyAsync(r => r.email == registerDTO.Email))
            {
                return BadRequest("Email đã được sử dụng bởi người dùng khác.");
            }

            // Kiểm tra định dạng email
            if (!IsValidEmail(registerDTO.Email))
            {
                return BadRequest("Email không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: example@domain.com).");
            }

            // Kiểm tra nếu password và confirmpassword khớp
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Mật khẩu và xác nhận mật khẩu không khớp.");
            }

            // Cập nhật thông tin tài khoản
            register.username = registerDTO.UserName;
            register.password = registerDTO.Password;
            register.confirmpassword = registerDTO.ConfirmPassword;
            register.email = registerDTO.Email;

            _context.Entry(register).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterExists(id))
                {
                    return NotFound("Không thể cập nhật vì tài khoản không tồn tại.");
                }
                else
                {
                    throw;
                }
            }

            // Cập nhật thông tin login liên quan
            var login = await _context.Logins.FindAsync(id);
            if (login != null)
            {
                login.username = register.username;
                login.password = register.password;
                _context.Entry(login).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            // Trả về phản hồi với thông báo thành công
            return Ok(new
            {
                Message = "Cập nhật tài khoản thành công",
                Data = registerDTO
            });
        }


        // DELETE: api/register/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegister(int id)
        {
            var register = await _context.Registers.FindAsync(id);
            if (register == null)
            {
                return NotFound();
            }

            _context.Registers.Remove(register);

            // Xóa bản ghi tương ứng trong bảng Login nếu có
            var login = await _context.Logins.FindAsync(id);
            if (login != null)
            {
                _context.Logins.Remove(login);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/register/logins
        [HttpGet("logins")]
        public async Task<ActionResult<IEnumerable<LoginDTO>>> GetLogins()
        {
            return await _context.Logins
                .Select(l => new LoginDTO
                {
                    UserName = l.username,
                    Password = l.password
                })
                .ToListAsync();
        }

        // GET: api/register/logins/{id}
        [HttpGet("logins/{id}")]
        public async Task<ActionResult<LoginDTO>> GetLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return new LoginDTO
            {
                UserName = login.username,
                Password = login.password
            };
        }

        [HttpPost("logins")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            // Tìm bản ghi Login theo username và password
            var login = await _context.Logins
                .FirstOrDefaultAsync(l => l.username == loginDTO.UserName && l.password == loginDTO.Password);

            if (login == null)
            {
                // Nếu không tìm thấy tài khoản khớp, trả về lỗi đăng nhập
                return BadRequest("Sai tài khoản hoặc mật khẩu.");
            }

            // Tìm thông tin đăng ký (Register) dựa trên customerid từ bản ghi Login
            var register = await _context.Registers
                .FirstOrDefaultAsync(r => r.customerid == login.customerid);

            if (register == null)
            {
                return BadRequest("Thông tin người dùng không tồn tại.");
            }

            // Trả về thông tin đăng ký của người dùng và thông báo đăng nhập thành công
            return Ok(new
            {
                Message = "Đăng nhập thành công",
                Data = new
                {
                    CustomerId = register.customerid,
                    UserName = register.username,
                    Password = register.password,
                    ConfirmPassword = register.confirmpassword,
                    Email = register.email
                    // Thêm các trường khác từ Register nếu cần
                }
            });
        }



        private bool RegisterExists(int id)
        {
            return _context.Registers.Any(e => e.customerid == id);
        }
    }
}
