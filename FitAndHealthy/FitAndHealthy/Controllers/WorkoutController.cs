using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitAndHealthy.Data;
using FitAndHealthy.Models;
using Microsoft.EntityFrameworkCore;
using FitAndHealthy.Service;


namespace FitAndHealthy.Controllers
{
    [LogExceptionService]
    [Produces("application/json")]
    [Route("api/Workout")]
    public class WorkoutController : Controller
    {
        private readonly WorkoutDataContext _context;

        public WorkoutController(WorkoutDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Workout> GetWorkout()
        {
            return _context.Workout;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkout([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workOut = await _context.Workout.SingleOrDefaultAsync(m => m.Id == id);
            if(workOut == null)
            {
                return NotFound();
            }
            return Ok(workOut);
        }

        
        [HttpPost]
        public async Task<IActionResult> PostWorkout([FromBody] Workout workout)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (workout.TimeInSeconds >= 5400)
            {
                throw new Exception("Sorry !!! We do't facilitate our users to jogg more than an hour. For WorkOut ID: "+ workout.Id);
            }
            _context.Workout.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = workout.Id }, workout);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout([FromRoute] int id,[FromBody] Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != workout.Id)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;
            try
            {
                var workOut = await _context.Workout.SingleOrDefaultAsync(m => m.Id == id);
                if(workout.TimeInSeconds >= 5400)
                {
                    throw new Exception("Sorry !!! We do't facilitate our users to jogg more than an hour. For WorkOut ID: " + workout.Id);
                }
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if(!WorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception();
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _context.Workout.SingleOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }
            _context.Workout.Remove(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);

        }

        private bool WorkoutExists(int id)
        {
            return _context.Workout.Any(e => e.Id == id);
        }
    }
}