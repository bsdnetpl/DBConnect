using DBConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DBConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MysqlController : ControllerBase
    {
        protected readonly ConnectToMysql _connectToMysql;
        protected readonly IPasswordHasher<Users> _passwordHasher;

        public MysqlController(ConnectToMysql connectToMysql, IPasswordHasher<Users> passwordHasher)
        {
            _connectToMysql = connectToMysql;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(Users user)
        {
            var result = await _connectToMysql.Users.FindAsync(user.Id);
            if(result == null)
            {
                user.password = _passwordHasher.HashPassword(user, user.password);
                _connectToMysql.Users.AddAsync(user);
                _connectToMysql.SaveChangesAsync();
                return Ok(user);

            }
            return StatusCode(401, "User in this Id exist");

        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var result = _connectToMysql.Users.ToList();
            if(result == null)
            {
                return StatusCode(401, "DB is Empty");
            }
            return Ok(result);
        }
    }
}
