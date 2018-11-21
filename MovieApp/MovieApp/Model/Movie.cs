using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Model
{
    public class Movie
    {
        public Movie()
        {
            this.MovieActorMappings = new HashSet<MovieActorMapping>();
            this.MovieProducerMappings = new HashSet<MovieProducerMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        public string Poster { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<MovieActorMapping> MovieActorMappings { get; set; }
        public virtual ICollection<MovieProducerMapping> MovieProducerMappings { get; set; }
    }
}
