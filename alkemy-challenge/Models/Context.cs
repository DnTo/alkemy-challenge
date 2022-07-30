using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace alkemy_challenge.Models
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        //businnes Entities
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Genre> Genres { get; set; }
        //App Entities
        public DbSet<User> Users { get; set; }


        //tablas many ti many
        public DbSet<CharacterMovie> CharacterMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //ef 3 

            modelBuilder.Entity<CharacterMovie>().HasKey(k => new { k.CharacterId, k.MovieId });

            modelBuilder.Entity<CharacterMovie>()
                 .HasOne(cm => cm.Movie)
                 .WithMany(c => c.CharacterMovies)
                 .HasForeignKey(f => f.CharacterId);

            modelBuilder.Entity<CharacterMovie>()
               .HasOne(cm => cm.Character)
               .WithMany(c => c.CharacterMovies)
               .HasForeignKey(f => f.CharacterId);

        }
    }
}
