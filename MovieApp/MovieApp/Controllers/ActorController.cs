using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Model;
using MovieApp.ViewModel;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Actor
        [HttpGet]
        public async Task<IEnumerable<MovieActor>> GetActors()
        {
            return await GetActorsListAsync();
        }

        // GET: api/Actor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        // PUT: api/Actor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor([FromRoute] int id, [FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.ActorId)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Actor
        [HttpPost]
        public async Task<IActionResult> PostActor([FromBody] MovieActor actorInfo)
        {
            Actor actor = new Actor
            {
                Name = actorInfo.Name,
                DOB = actorInfo.DOB,
                Gender = actorInfo.Gender,
                Bio = actorInfo.Bio,
                CreatedDate = DateTime.Now
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.ActorId }, actor);
        }

        // DELETE: api/Actor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return Ok(actor);
        }

        #region Private Methods
        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorId == id);
        }

        private async Task<IEnumerable<MovieActor>> GetActorsListAsync()
        {
            List<Actor> actors = await _context.Actors.ToListAsync();

            List<MovieActor> movieActors = actors
                                            .Select(t =>
                                            {
                                                return new MovieActor
                                                {
                                                    ActorId = t.ActorId,
                                                    Name = t.Name,
                                                    DOB = t.DOB,
                                                    Gender = t.Gender,
                                                    Bio = t.Bio
                                                };
                                            }).ToList();

            return movieActors;
        }
        #endregion Private Methods

    }
}