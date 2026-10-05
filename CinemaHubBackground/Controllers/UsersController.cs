using CinemaHubBackground.Data;
using CinemaHubShared.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHubBackground.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            User? user = await (from u in _context.Users
                               join l in _context.Logins on u.FK_Login equals l.Login_Id
                               where l.Login_Id == request.Email && l.Password == request.Password
                               select u).FirstOrDefaultAsync();
            if(user == null)
            {
                return Unauthorized(new {message = "Неверный логин или пароль"});
            }
            else
            {
                return Ok(user);
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var existingLogin = await _context.Logins.FirstOrDefaultAsync(l => l.Login_Id == request.Email);

            if(existingLogin != null)
            {
                return BadRequest(new { message = "Пользователь с таким email уже существует!" });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newLogin = new Login
                {
                    Login_Id = request.Email,
                    Password = request.Password
                };
                _context.Logins.Add(newLogin);
                await _context.SaveChangesAsync();

                var newUser = new User
                {
                    Family = request.Family,
                    Name = request.Name,
                    Father = request.Father,
                    FK_Role = 1,
                    FK_Login = newLogin.Login_Id,
                    Date_registration = DateTime.UtcNow
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new { message = "Успешная регистрация!" });
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = $"Ошибка на сервере: {ex.Message}" });
            }
        }
    }
}
