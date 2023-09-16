using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Helpers;
using pet911_backend.Models;
using pet911_backend.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pet911_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Role
        [HttpGet]
        public ActionResult<Role> Get()
        {
            try
            {
                List<Role> roles = _context.Role.ToList();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/Role/5
        [HttpGet("{id}")]
        public ActionResult<Role> Get(string id)
        {
            try
            {
                List<Role> roles = _context.Role.Where(x=>x.Id == id).ToList();

                return Ok(roles[0]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Role
        [HttpPost]
        public ActionResult Post([FromBody] RoleDto role)
        {
            try
            {
                 Role newRol = new Role();
                newRol.Id = Guid.NewGuid().ToString();
                newRol.RoleType = role.RoleType;
                newRol.Description = role.Description;
                _context.Role.Add(newRol);
                _context.SaveChanges();
                return Ok(newRol);
            }catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        

        }

        // PUT api/Role/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] RoleDto role)
        {
            try
            {
                var oldRole = _context.Role.Find(id);

                if (oldRole == null)
                {
                  return  BadRequest("No se econtro el rol");
                }

                oldRole.RoleType = role.RoleType;
                oldRole.Description = role.Description;
                _context.SaveChanges();
                return Ok(oldRole);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/Role/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var role = _context.Role.Find(id);
                if (role == null)
                {
                  return  BadRequest("No se econtro el rol");

                }
                _context.Role.Remove(role);
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
