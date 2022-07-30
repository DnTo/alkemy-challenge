using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alkemy_challenge.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}