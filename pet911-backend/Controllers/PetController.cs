using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pet911_backend.Helpers;
using pet911_backend.Models;
using pet911_backend.Models.Dto;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pet911_backend.Controllers
{
    //[Authorize(Roles ="Admin,Usuario")]
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly DataContext _context;

        public PetController(DataContext context)
        {
            _context = context;
        }
        // GET: api/<PetController>
        [HttpGet]
        public ActionResult<Role> GetPet()
        {
            try
            {
                List<Pet> pets = _context.Pet.ToList();

                return Ok(pets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PetController>/5
        [HttpGet("{id}")]
        public ActionResult<Pet> GetOnePet(string id)
        {
            try
            {
                Pet pets = _context.Pet.Find(id);
                if (pets == null)
                {
                    return BadRequest("No existe");
                }

                return Ok(pets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("ByUser/{id}")]
        public ActionResult<Pet> GetPetsByUser(string id)
        {
            try
            {
                List<Pet> pets = _context.Pet.Where(pet=>pet.IdUser==id).ToList();
                if (pets == null || pets.Count ==0)
                {
                    return BadRequest("No tiene pets");
                }
                return Ok(pets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PetController>
        [HttpPost]
        public ActionResult<Pet> PostPet([FromBody] PetDto model)
        {
            User use = _context.User.Where(us => us.Email == model.Email).FirstOrDefault();
            try
            {
                Pet newPet = new Pet();
                newPet.Id = Guid.NewGuid().ToString();
                newPet.Name = model.Name;
                newPet.Age = model.Age;
                newPet.Sex = model.Sex;
                newPet.Race = model.Race;
                newPet.Allergies = model.Allergies;
                newPet.IdUser = use.Id;

                _context.Pet.Add(newPet);
                _context.SaveChanges();
                return Ok(newPet);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<PetController>/5
        [HttpPut("{id}")]
        public ActionResult<Pet> PutPet(string id, [FromBody] PetDto model)
        {
            User use = _context.User.Where(us => us.Email == model.Email).FirstOrDefault();
            var oldPet = _context.Pet.Find(id);
            if(oldPet == null)
            {
                return BadRequest("No existe");
            }
            try
            {
                oldPet.Name = model.Name;
                oldPet.Age = model.Age;
                oldPet.Sex = model.Sex;
                oldPet.Race = model.Race;
                oldPet.Allergies = model.Allergies;
                oldPet.IdUser = use.Id;

                _context.SaveChanges();
                return Ok(oldPet);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/<PetController>/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> DeletePet(string id)
        {
            try
            {
                var pet = _context.Pet.Find(id);
                if (pet == null)
                {
                    return BadRequest("No se econtro pet");

                }
                _context.Pet.Remove(pet);
                _context.SaveChanges();
                return Ok("Se elimino pet");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
