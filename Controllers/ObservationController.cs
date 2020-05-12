using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public void Post([FromBody] Measurement value)
        {
            _context.Entry(value.Place).State = EntityState.Unchanged;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Measurements.Add(value);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
        }

    }
}
