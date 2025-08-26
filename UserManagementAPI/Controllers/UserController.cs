using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Nuntipat", Username = "Gett", Email = "nuntipat.ptr@gmail.com", Phone = "0907169189", Website = "www.github.com/ggetnunti" },
            new User { Id = 2, Name = "Pantarag", Username = "Nim", Email = "Ar-titaya@bangkokbank.com", Phone = "0634035669", Website = "www.bangkokbang.com" }
        };

        // GET api/users
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(users);
        }

        // GET api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("404 Not Found");
            }
            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Name, Username, and Email are required.");
            }
            user.Id = users.Max(u => u.Id) + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT api/users/1
        [HttpPut("{id}")]
        public ActionResult UpdateUser(long id, User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound("404 Not Found");
            }

            existingUser.Name = user.Name;
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.Website = user.Website;

            return NoContent();
        }

        // DELETE api/users/1
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(long id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("404 Not Found");
            }
            users.Remove(user);
            return NoContent();
        }
    }
}
