using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGK_Assignment_3.Areas.Database;
using NGK_Assignment_3.Areas.Database.Models;

namespace NGK_Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private NGKDbContext _context;
        public ObservationController(NGKDbContext context)
        {
            _context = context;
            var brabrand = _context.Places.Where(p => p.Name == "Brabrand").FirstOrDefault();
            var dummy1 = new Measurement() { Humidity = 26, Place = brabrand, Pressure = 101200, Temperature = 17, Time = DateTime.Now.AddDays(1) };
            _context.Measurements.Add(dummy1);
            _context.SaveChanges();
        }
        // GET: api/Observation
        [HttpGet]
        public ActionResult<Measurement> Get()
        {
            return _context.Measurements.FirstOrDefault();
        }

        // GET: api/Observation/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Observation
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Observation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
