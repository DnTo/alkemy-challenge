using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alkemy_challenge.Models;
using Microsoft.AspNetCore.Authorization;

namespace alkemy_challenge.Controllers
{  
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly Context _context;

        public CharactersController(Context context)
        {
            _context = context;
        }

        // GET: api/Characters
        [HttpGet]
        
        public async Task<JsonResult> GetCharacters()
        {
            return  new JsonResult( await _context
                .Characters
                .Select(x => new { id = x.CharacterId, name = x.Name, x.Image })
                .ToListAsync());
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetCharacter(int id)
        {
            var character =  _context.Characters.Include("CharacterMovies").FirstOrDefault(x => x.CharacterId == id);

            if (character == null)
            {

                return NotFound(Json("404");
            }

            //get movies
            var movies = character.CharacterMovies.Select(m => new { m.Movie.Title }).ToList();
            return new JsonResult(new { character = character, movies = movies });
            //return  character;
            
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacter", new { id = character.CharacterId }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return character;
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.CharacterId == id);
        }
    }
}
