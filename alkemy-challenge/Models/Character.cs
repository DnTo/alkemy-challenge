using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Models
{
    public class Character
    {

        public Character()
        {
            this.CharacterMovies = new HashSet<CharacterMovie>();
        }

        public int CharacterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [NotMapped]
        public int Age
        {
            get
            {
                var _months = DateTime.Now.Month - BirthDate.Month;
                var _years = DateTime.Now.Year - BirthDate.Year;
                if (_years < 0) throw new Exception("Nacimiento debe ser anterior a hoy");

                return (_months < 0) ? _years - 1 : _years;
            }
        }

        [Required]
        public int Weigth { get; set; }
        [Required]
        public string History { get; set; }
        public virtual IEnumerable<CharacterMovie> CharacterMovies { get; set; }
    }
}
