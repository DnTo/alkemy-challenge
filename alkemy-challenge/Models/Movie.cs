using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Models
{
    public class Movie
    {
        public Movie()
        {
            this.CharacterMovies = new HashSet<CharacterMovie>();
        }


        public int MovieId { get; set; }
        [Required]
        public string Name { get; set; }

        public int GenreId { get; set; }

       // [Index]
        public Genre Genre { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Range(1.0, 5.0, ErrorMessage = "Invalid value for rating(1 to 5)")]
        public int Rating { get; set; }
        [Required]
        public string Image { get; set; }


        public virtual ICollection<CharacterMovie> CharacterMovies { get; set; }

    }

}
