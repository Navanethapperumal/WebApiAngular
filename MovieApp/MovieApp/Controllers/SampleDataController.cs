using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Model;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly Model.MovieContext context;

        public SampleDataController(Model.MovieContext context)
        {
            this.context = context;
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            /*
            context.Add(new Producer { Name = "Siva", DOB = Convert.ToDateTime("01/01/2018"), Gender = "Male", Bio = "God", CreatedDate = DateTime.Now });
            context.Add(new Model.Actor { Name = "Navaneethapperumal", DOB = Convert.ToDateTime("18/11/2018"), Gender = "Male", Bio= "", CreatedDate = DateTime.Now });

            context.Add(new Movie { Name = "Life", Plot = "Destiny", CreatedDate = DateTime.Now, ReleaseDate = DateTime.Now, Poster = "" });
            context.Add(new Movie { Name = "Future", Plot = "Destiny", CreatedDate = DateTime.Now, ReleaseDate = DateTime.Now, Poster = "" });

            context.SaveChanges();

            int actorId = context.Actors.Where(t => t.Name == "Navaneethapperumal").SingleOrDefault().ActorId;
            int producerId = context.Producers.Where(t => t.Name == "Siva").SingleOrDefault().ProducerId;
            int movieId = context.Movies.Where(t => t.Name == "Life").SingleOrDefault().MovieId;

            MovieActorMapping ma = new MovieActorMapping { ActorId = actorId, MovieId = movieId };
            MovieProducerMapping mp = new MovieProducerMapping { MovieId = movieId, ProducerId = producerId };

            context.Add(ma);
            context.Add(mp);
            context.SaveChanges();

            movieId = context.Movies.Where(t => t.Name == "Future").SingleOrDefault().MovieId;
            ma = new MovieActorMapping { ActorId = actorId, MovieId = movieId };
            mp = new MovieProducerMapping { MovieId = movieId, ProducerId = producerId };
            context.Add(ma);
            context.Add(mp);
            context.SaveChanges();

            context.Add(new Model.Actor { Name = "Kanimuthu", DOB = Convert.ToDateTime("18/11/2018"), Gender = "Female", Bio = "", CreatedDate = DateTime.Now });
            context.SaveChanges();
            int actorId = context.Actors.Where(t => t.Name == "Kanimuthu").SingleOrDefault().ActorId;
            int movieId = context.Movies.Where(t => t.Name == "Life").SingleOrDefault().MovieId;

            MovieActorMapping ma = new MovieActorMapping { ActorId = actorId, MovieId = movieId };
            context.Add(ma);
            context.SaveChanges();
            */


            List<Movie> movies = context.Movies.ToList();
            List<Producer> producers = context.Producers.ToList();
            List<Actor> actors = context.Actors.ToList();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
