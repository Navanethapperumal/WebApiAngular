using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Model
{
    public class Actor
    {
        public Actor()
        {
            this.MovieActorMappings = new HashSet<MovieActorMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<MovieActorMapping> MovieActorMappings { get; set; }
    }
}
