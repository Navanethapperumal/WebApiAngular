using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.ViewModel
{
    public abstract class Person
    {
        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public string Bio { get; set; }
    }

    public class MovieActor : Person
    {
        public int ActorId { get; set; }
    }

    public class MovieProducer : Person
    {
        public int ProducerId { get; set; }
    }

    public class MovieInformation
    {
        public int MovieId { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseYear { get; set; }

        public string Plot { get; set; }

        public string Poster { get; set; }

        public List<MovieActor> Actors { get; set; }

        public MovieProducer Producer { get; set; }
    }
}
