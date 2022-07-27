using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }


        public DateTime BirthDate { get; set; }
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

        public int Weigth { get; set; }
        public string History { get; set; }
        public IList<Movie> MoviesAppeared { get; set; }
    }
}
