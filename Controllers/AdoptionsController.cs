using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using PetAdoptionAPI.Models;
using System.Threading.Tasks;

namespace PetAdoptionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionsController : ControllerBase
    {
        private readonly PetAdoptionContext _context;

        public AdoptionsController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: api/Adoptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adoptions>>> GetAdoptions()
        {
            return await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .ToListAsync();
        }

        // GET: api/Adoptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adoptions>> GetAdoption(int id)
        {
            var adoption = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.AdoptionID == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return adoption;
        }
        

        // POST: api/Adoptions
        [HttpPost]
        public async Task<ActionResult<Adoptions>> PostAdoption(Adoptions adoption)
        {
            // Check if the UserID exists
            var user = await _context.Users.FindAsync(adoption.UserID);
            if (user == null)
            {
                return BadRequest("Invalid UserID.");
            }

            // Check if the PetID exists
            var pet = await _context.Pets.FindAsync(adoption.PetID);
            if (pet == null)
            {
                return BadRequest("Invalid PetID.");
            }

            // Populate the UserName and PetName
            adoption.UserName = user.Name;
            adoption.PetName = pet.Name;

            // Assign navigation properties
            adoption.User = user;
            adoption.Pet = pet;

            // Add and save the adoption
            _context.Adoptions.Add(adoption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdoption", new { id = adoption.AdoptionID }, adoption);
        }

        // PUT: api/Adoptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoption(int id, Adoptions adoption)
        {
            if (id != adoption.AdoptionID)
            {
                return BadRequest();
            }

            // Check if the UserID exists
            var user = await _context.Users.FindAsync(adoption.UserID);
            if (user == null)
            {
                return BadRequest("Invalid UserID.");
            }

            // Check if the PetID exists
            var pet = await _context.Pets.FindAsync(adoption.PetID);
            if (pet == null)
            {
                return BadRequest("Invalid PetID.");
            }

            // Update the UserName and PetName (optional if you allow them to be updated)
            adoption.UserName = user.Name;
            adoption.PetName = pet.Name;

            // Assign the navigation properties (again optional depending on your business rules)
            adoption.User = user;
            adoption.Pet = pet;

            // Mark the adoption entity as modified
            _context.Entry(adoption).State = EntityState.Modified;

            try
            {
                // Save changes asynchronously
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Return OK (200) with the updated adoption object
            return Ok(adoption);
        }



        // Helper method to check if an adoption exists
        private bool AdoptionExists(int id)
        {
            return _context.Adoptions.Any(e => e.AdoptionID == id);
        }


        // DELETE: api/Adoptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoption(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }

            _context.Adoptions.Remove(adoption);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
