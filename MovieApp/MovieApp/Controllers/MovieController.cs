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

        [HttpGet]
        [Route("actors")]
        public async Task<IEnumerable<MovieActor>> GetActors()
        {
            return await GetActorsList();
        }

        [HttpGet]
        [Route("producers")]
        public async Task<IEnumerable<MovieProducer>> GetProducers()
        {
            return await GetProducersList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<MovieInformation> Get([FromRoute] int id)
        {
            return await GetMovie(id);
        }
        
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
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


                movie.Actors = actors.Join(movieActors, a=>a.ActorId, ma=>ma.ActorId, (a, ma)=>new { actor = a, actorMovies = ma })
                    .Where(t=>t.actorMovies.MovieId == item.MovieId)
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

            return movies.Where(t=>t.MovieId == id).FirstOrDefault();
        }

        private async Task<IEnumerable<MovieActor>> GetActorsList()
        {
            List<Actor> actors = await context.Actors.ToListAsync();

            List<MovieActor> movieActors = actors
                                            .Select(t =>
                                            {
                                                return new MovieActor
                                                {
                                                    ActorId = t.ActorId,
                                                    Name = t.Name,
                                                    DOB = t.DOB,
                                                    Gender = t.Gender,
                                                    Bio= t.Bio
                                                };
                                            }).ToList();

            return movieActors;
        }

        private async Task<IEnumerable<MovieProducer>> GetProducersList()
        {
            List<Producer> producers = await context.Producers.ToListAsync();
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
