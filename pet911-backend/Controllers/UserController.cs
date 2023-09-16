using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Helpers;
using pet911_backend.Models;
using pet911_backend.Models.Dto;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pet911_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        // GET: api/User
        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            try
            {
                List<User> users = _context.User.ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/User/5
        [HttpGet("{id}")]
        public ActionResult<User> GetOneUser(string id)
        {
            try
            {
                User user = _context.User.Find(id);
                if (user == null)
                {
                    return BadRequest("No existe");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/User
        [HttpPost]
        public ActionResult Post([FromBody] UserDto user)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));
                }
                User newUser = new User();
                newUser.Id = Guid.NewGuid().ToString();
                newUser.Name = user.Name;
                newUser.Email = user.Email;
                newUser.Birthdate= user.Birthdate;
                newUser.PasswordSalt = passwordSalt;
                newUser.PasswordHash = passwordHash;
                newUser.IdRole = user.IdRole;
               
                _context.User.Add(newUser);
                _context.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
           

        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] UserDto model)
        {
            var oldUser = _context.User.Find(id);
            if (oldUser == null)
            {
                return BadRequest("No existe");
            }
            try
            {
                byte[] passwordHash, passwordSalt;
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                }
                oldUser.Name = model.Name;
                oldUser.Email = model.Email;
                oldUser.Birthdate = model.Birthdate;
                oldUser.PasswordSalt = passwordSalt;
                oldUser.PasswordHash = passwordHash;
                oldUser.IdRole = model.IdRole;

                _context.SaveChanges();
                return Ok(oldUser);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var user = _context.User.Find(id);
                if (user == null)
                {
                    return BadRequest("No se econtro el rol");

                }
                _context.User.Remove(user);
                _context.SaveChanges();
                return Ok("Se elimino el rol");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
