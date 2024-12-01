using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using PetAdoptionAPI.Models;


namespace PetAdoptionAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {

        private readonly PetAdoptionContext _context;
        public PetsController(PetAdoptionContext context)
        {
            _context = context;
        }


        // GET: api/Pets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pets>>>GetPet()
        {
            return await _context.Pets.ToListAsync();
        }
        

        // GET: api/Pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pets>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return pet;
        }
        

        // POST: api/Pets
        [HttpPost]
        public async Task<ActionResult<Pets>>PostPet(Pets pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPet", new {id = pet.PetID}, pet);
        }

        // PUT: api/Pets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pets pet)
        {
            if (id != pet.PetID)
            {
                return BadRequest();
            }

            // Attach and mark the pet entity as modified
            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                // Save changes asynchronously
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return OK (200) with the updated pet object
            return Ok(pet);
        }

        // Helper method to check if a pet exists
        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetID == id);
        }


        // DELETE: api/Pets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
