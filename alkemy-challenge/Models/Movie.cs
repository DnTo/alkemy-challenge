using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre Genre { get; set; }


        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, 5,"Invalid value for rating(1 to 5)")]
        public int Rating { get; set; }

        public string Image { get; set; }

        public IList<Character> Characters { get; set; }
    }
}
