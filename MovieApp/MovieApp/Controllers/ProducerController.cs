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
    public class ProducerController : ControllerBase
    {
        private readonly MovieContext _context;

        public ProducerController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Producer
        [HttpGet]
        public async Task<IEnumerable<MovieProducer>> GetProducers()
        {
            return await GetProducersListAsync();
        }

        // GET: api/Producer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producer = await _context.Producers.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return Ok(producer);
        }

        // PUT: api/Producer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer([FromRoute] int id, [FromBody] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producer.ProducerId)
            {
                return BadRequest();
            }

            _context.Entry(producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
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

        // POST: api/Producer
        [HttpPost]
        public async Task<IActionResult> PostProducer([FromBody] MovieProducer producerInfo)
        {
            Producer producer = new Producer
            {
                Name = producerInfo.Name,
                DOB = producerInfo.DOB,
                Gender = producerInfo.Gender,
                Bio = producerInfo.Bio,
                CreatedDate = DateTime.Now
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducer", new { id = producer.ProducerId }, producer);
        }

        // DELETE: api/Producer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();

            return Ok(producer);
        }

        #region Private Methods
        private bool ProducerExists(int id)
        {
            return _context.Producers.Any(e => e.ProducerId == id);
        }

        private async Task<IEnumerable<MovieProducer>> GetProducersListAsync()
        {
            List<Producer> producers = await _context.Producers.ToListAsync();
            List<MovieProducer> movieProducers = producers
                                            .Select(t =>
                                            {
                                                return new MovieProducer
                                                {
                                                    ProducerId = t.ProducerId,
                                                    Name = t.Name,
                                                    DOB = t.DOB,
                                                    Gender = t.Gender,
                                                    Bio = t.Bio
                                                };
                                            }).ToList();

            return movieProducers;
        }
        #endregion Private Methods
    }
}