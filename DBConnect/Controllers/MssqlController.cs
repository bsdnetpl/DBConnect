using DBConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DBConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MssqlController : ControllerBase
    {
        protected readonly ConnectToMssql _connectToMssql;
        protected readonly IPasswordHasher<Users> _passwordHasher;

        public MssqlController(ConnectToMssql connectToMssql, IPasswordHasher<Users> passwordHasher)
        {
            _connectToMssql = connectToMssql;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Users user)
        {
            var result = await _connectToMssql.Users.FindAsync(user.Id);
            if(result is null)
            {
                user.password = _passwordHasher.HashPassword(user, user.password);
                _connectToMssql.Users.AddAsync(user);
                _connectToMssql.SaveChangesAsync();
                return Ok(user);
                
            }
            return StatusCode(401, "This user exist on the DB");
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var result = _connectToMssql.Users.ToList();
            return Ok(result);

        }

    }
}
