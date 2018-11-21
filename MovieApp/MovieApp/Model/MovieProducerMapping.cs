using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Model
{
    public class MovieProducerMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Movies"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MovieId { get; set; }

        [ForeignKey("Producers"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
