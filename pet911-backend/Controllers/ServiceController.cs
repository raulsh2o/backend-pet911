using Microsoft.AspNetCore.Authorization;
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
    public class ServiceController : ControllerBase
    {
        private readonly DataContext _context;

        public ServiceController(DataContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
        // GET: api/Service
        [HttpGet]
        public ActionResult<Service> GetServices()
        {
            try
            {
                List<Service> services = _context.Service.ToList();

                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        // GET api/Service/5
        [HttpGet("{id}")]
        public ActionResult<Service> GetOneService(string id)
        {
            try
            {
                Service services = _context.Service.Find(id);
                if (services == null)
                {
                    return NotFound();
                }
                return Ok(services);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/Service
        [HttpPost]
        public ActionResult<Service> PostService([FromBody] ServiceDto model)
        {
            try
            {

                Service newService = new Service();
                
                newService.Id = Guid.NewGuid().ToString(); ;
                newService.Name = model.Name;
                newService.Type = model.Type;
                newService.OpeningTime = model.OpeningTime;
                newService.ClosingTime  = model.ClosingTime;
                newService.ContactNumber = model.ContactNumber;
                newService.Latitude = model.Latitude;
                newService.Longitude = model.Longitude;
                newService.Catalogue = model.Catalogue;
                newService.Sponsored = model.Sponsored;
                newService.IdUser = model.IdUser;


                _context.Service.Add(newService);
                _context.SaveChanges();
                return Ok(newService);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/Service/5
        [HttpPut("{id}")]
        public ActionResult<Service> PutService(string id, [FromBody] ServiceDto model)
        {
            try
            {
                var oldService = _context.Service.Find(id);
                if (oldService == null)
                {
                    return BadRequest("No existe");
                }
                oldService.Name = model.Name;
                oldService.Type = model.Type;
                oldService.OpeningTime = model.OpeningTime;
                oldService.ClosingTime = model.ClosingTime;
                oldService.ContactNumber = model.ContactNumber;
                oldService.Latitude = model.Latitude;
                oldService.Longitude = model.Longitude;
                oldService.Catalogue = model.Catalogue;
                oldService.Sponsored = model.Sponsored;
                oldService.IdUser = model.IdUser;

                _context.SaveChanges();
                return Ok(oldService);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/Service/5
        [HttpDelete("{id}")]
        public ActionResult DeleteService(string id)
        {
            try
            {
                var service = _context.Service.Find(id);
                if (service == null)
                {
                    return BadRequest("No se econtro el service");

                }
                _context.Service.Remove(service);
                _context.SaveChanges();
                return Ok("Se elimino el Service");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

