using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace alkemy_challenge.Models
{
    public class CharacterMovie
    {
        [Key,Column(Order=1)]
        public virtual int CharacterId { get; set; }
        [Key, Column(Order = 2)]
        public virtual int MovieId { get; set; }
        public Character Character { get; set; }
        public Movie Movie { get; set; }

    }
}
