using System.Collections.Generic;

namespace alkemy_challenge.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}