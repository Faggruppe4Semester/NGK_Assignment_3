using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NGK_Assignment_3.Areas.Database;
using NGK_Assignment_3.Areas.Database.Models;
using NGK_Assignment_3.Hubs;

namespace NGK_Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private NGKDbContext _context;
        private readonly IHubContext<MeasurementHub> _hub;
        public ObservationController(NGKDbContext context, IHubContext<MeasurementHub> hub)
        {
            _hub = hub;
            _context = context;
            _context.SaveChanges();
        }
        // GET: api/Observation
        [HttpGet]
        public ActionResult<List<Measurement>> Get()
        {
            return _context.Measurements
                .Include(m => m.Place)
                .OrderByDescending(M => M.Time)
                .Take(3)
                .ToList();
        }

        // GET: api/Observation/MM-DD-YYYY
        [HttpGet("{date}")]
        public ActionResult<List<Measurement>> Get(DateTime date)
        {
            return _context.Measurements
                .Include(m => m.Place)
                .OrderByDescending(m => m.Time)
                .Where(m => m.Time.Date == date.Date)
                .ToList();
        }

        // GET: api/Observation/MM-DD-YYYY/MM-DD-YYYY
        [HttpGet("{startTime}/{endTime}")]
        public ActionResult<List<Measurement>> Get(DateTime startTime, DateTime endTime)
        {
            return _context.Measurements
                .Include(m => m.Place)
                .OrderByDescending(m => m.Time)
                .Where(m => m.Time >= startTime &&  m.Time <= endTime)
                .ToList();
        }

        // POST: api/Observation
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Measurement value)
        {
            var place = _context.Places.AsNoTracking().FirstOrDefault(p => p.Name == value.Place.Name);
            if (place == null)
            {
                _context.Add(value.Place);
                _context.SaveChanges();
            }
            else
            {
                _context.Entry(value.Place).State = EntityState.Unchanged;
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Measurements.Add(value);
                    _context.SaveChanges();
                    transaction.Commit();
                    await _hub.Clients.All.SendAsync("ReceiveMeasurement", value.Time, value.Place.Name,value.PlaceLat,value.PlaceLon,value.Temperature,value.Humidity,value.Pressure);
                    return Ok();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return NotFound();
                }
            }
        }

        [HttpGet("SeedData")]
        public ActionResult SeedData()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var p1 = new Place()
                    {
                        Lat = 4,
                        Lon = 20,
                        Name = "Amsterdam"
                    };
                    var p2 = new Place()
                    {
                        Lat = 66,
                        Lon = 25,
                        Name = "Brabrand"
                    };

                    var m1 = new Measurement()
                    {
                        Humidity = 25,
                        Place = p2,
                        Pressure = 1050,
                        Temperature = 13,
                        Time = DateTime.Now.AddDays(-1)
                    };

                    var m2 = new Measurement()
                    {
                        Humidity = 19,
                        Place = p2,
                        Pressure = 1080,
                        Temperature = 18,
                        Time = DateTime.Now.AddDays(1)
                    };

                    var m3 = new Measurement()
                    {
                        Humidity = 45,
                        Place = p1,
                        Pressure = 1020,
                        Temperature = 8,
                        Time = DateTime.Now.AddDays(-10)
                    };

                    var m4 = new Measurement()
                    {
                        Humidity = 30,
                        Place = p1,
                        Pressure = 1030,
                        Temperature = 13,
                        Time = DateTime.Now.AddDays(-5)
                    };

                    _context.Add(m1);
                    _context.Add(m2);
                    _context.Add(m3);
                    _context.Add(m4);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e);
                    return BadRequest();
                }
            }
        }

    }
}
