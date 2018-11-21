using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Model
{
    public class Producer
    {
        public Producer()
        {
            this.MovieProducerMappings = new HashSet<MovieProducerMapping>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProducerId { get; set; }
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

        public virtual ICollection<MovieProducerMapping> MovieProducerMappings { get; set; }
    }
}
