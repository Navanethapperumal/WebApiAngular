using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Model;
using MovieApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieContext context;

        public MovieController(MovieContext context)
        {
            this.context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<MovieInformation>> Get()
        {
            return await GetMovieList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] MovieInformation movieInfo)
        {
            Movie movie = new Movie
            {
                Name = movieInfo.Name,
                Plot = movieInfo.Plot,
                Poster = movieInfo.Poster,
                ReleaseDate = movieInfo.ReleaseYear,
                CreatedDate = DateTime.Now
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Movies.Add(movie);

            try
            {
                await context.SaveChangesAsync();

                int movieId = movie.MovieId;

                List<MovieActorMapping> movieActors = movieInfo.Actors.Select(t => { return new MovieActorMapping { ActorId = t.ActorId, MovieId = movieId }; }).ToList();
                MovieProducerMapping movieProducer = new MovieProducerMapping { MovieId = movieId, ProducerId = movieInfo.Producer.ProducerId };

                await context.MovieActorMappings.AddRangeAsync(movieActors);
                await context.MovieProducerMappings.AddAsync(movieProducer);

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie([FromRoute] int id, [FromBody] MovieInformation movieInfo)
        {
            Movie movie = new Movie
            {
                MovieId = movieInfo.MovieId,
                Name = movieInfo.Name,
                Plot = movieInfo.Plot,
                Poster = movieInfo.Poster,
                ReleaseDate = movieInfo.ReleaseYear
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            List<MovieActorMapping> movieActors = await context.MovieActorMappings.Where(t => t.MovieId == movie.MovieId).ToListAsync();
            MovieProducerMapping movieProducer = await context.MovieProducerMappings.Where(t => t.MovieId == movie.MovieId).FirstOrDefaultAsync();

            List<MovieActorMapping> removeMovieActors = movieActors.FindAll(t => !movieInfo.Actors.Exists(s => s.ActorId == t.ActorId)).ToList();
            List<MovieActorMapping> newMovieActors = movieInfo.Actors.FindAll(t => !movieActors.Exists(s => s.ActorId == t.ActorId))
                .Select(t => { return new MovieActorMapping { ActorId = t.ActorId, MovieId = movieInfo.MovieId }; })
                .ToList();


            if(movieProducer.ProducerId != movieInfo.Producer.ProducerId)
            {
                MovieProducerMapping newProducer = new MovieProducerMapping
                {
                    MovieId = movieInfo.MovieId,
                    ProducerId = movieInfo.Producer.ProducerId
                };
                context.MovieProducerMappings.Remove(movieProducer);

                await context.MovieProducerMappings.AddAsync(newProducer);
            }

            context.Entry(movie).State = EntityState.Modified;
            context.MovieActorMappings.RemoveRange(removeMovieActors);
            await context.MovieActorMappings.AddRangeAsync(newMovieActors);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #region Private Methods
        private async Task<IEnumerable<MovieInformation>> GetMovieList()
        {
            List<Movie> movies = await context.Movies.ToListAsync();
            List<Actor> actors = await context.Actors.ToListAsync();
            List<Producer> producers = await context.Producers.ToListAsync();
            List<MovieActorMapping> movieActors = await context.MovieActorMappings.ToListAsync();
            List<MovieProducerMapping> movieProducers = await context.MovieProducerMappings.ToListAsync();

            List<MovieInformation> movieList = new List<MovieInformation>();

            foreach (var item in movies)
            {
                MovieInformation movie = new MovieInformation
                {
                    MovieId = item.MovieId,
                    Name = item.Name,
                    ReleaseYear = item.ReleaseDate,
                    Plot = item.Plot,
                    Poster = item.Poster
                };


                movie.Actors = actors.Join(movieActors, a => a.ActorId, ma => ma.ActorId, (a, ma) => new { actor = a, actorMovies = ma })
                    .Where(t => t.actorMovies.MovieId == item.MovieId)
                    .Select(t =>
                {
                    return new MovieActor { ActorId = t.actor.ActorId, Name = t.actor.Name, Gender = t.actor.Gender, DOB = t.actor.DOB, Bio = t.actor.Bio };
                }).ToList();

                movie.Producer = producers.Join(movieProducers, p => p.ProducerId, mp => mp.ProducerId, (p, mp) => new { producer = p, movieProducer = mp })
                   .Where(t => t.movieProducer.MovieId == item.MovieId)
                   .Select(t =>
                   {
                       return new MovieProducer
                       {
                           ProducerId = t.producer.ProducerId,
                           Name = t.producer.Name,
                           DOB = t.producer.DOB,
                           Gender = t.producer.Gender,
                           Bio = t.producer.Bio
                       };
                   }).FirstOrDefault();

                movieList.Add(movie);
            }

            return movieList;
        }

        private async Task<MovieInformation> GetMovie(int id)
        {
            IEnumerable<MovieInformation> movies = await GetMovieList();

            return movies.Where(t => t.MovieId == id).FirstOrDefault();
        }

        private bool MovieExists(int id)
        {
            return context.Movies.Any(e => e.MovieId == id);
        }

        #endregion Private Methods
    }
}
