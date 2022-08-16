using DBConnect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult> AddUsers(Users user)
        {
            var result = await _connectToMysql.Users.FindAsync(user.Id);
            if (result == null)
            {
                user.password = _passwordHasher.HashPassword(user, user.password);
                _connectToMysql.Users.Add(user);
                _connectToMysql.SaveChanges();
                return Ok("User Add correctly !!");
            }
            return NoContent();

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
        [HttpGet("GetByID")]
        public IActionResult Get(Guid id)
        {
            var result = _connectToMysql.Users.Find(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var result = _connectToMysql.Users.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            _connectToMysql.Users.Remove(result);
            _connectToMysql.SaveChanges();
            return Ok($"User number {id} has been deleted");
        }

    }
}
