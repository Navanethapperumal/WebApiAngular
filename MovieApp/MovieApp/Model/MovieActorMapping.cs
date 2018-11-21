using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Model
{
    public class MovieActorMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Movies"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }

        [ForeignKey("Actors"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActorId { get; set; }

        public virtual Actor Actor { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
