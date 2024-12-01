using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using PetAdoptionAPI.Models;

namespace PetAdoptionAPI.Controllers
{
    public class PetsMvcController : Controller
    {
        private readonly PetAdoptionContext _context;

        public PetsMvcController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: PetsMvc
        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets.ToListAsync();
            return View(pets); // Return the list of all pets
        }

        // GET: PetsMvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.PetID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet); // Return the pet details view
        }

        // GET: PetsMvc/Create
        public IActionResult Create()
        {
            return View(); // Render the Create form
        }

        // POST: PetsMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetID,Name,Age,Breed,AdoptionStatus")] Pets pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pet created successfully!"; // Success message
                return RedirectToAction(nameof(Index)); // Redirect to the pets list
            }
            return View(pet); // Return to form if invalid data
        }


        // GET: PetsMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet); // Render the Edit form for the pet
        }

        // POST: PetsMvc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,Name,Age,Breed,AdoptionStatus")] Pets pet)
        {
            if (id != pet.PetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Pet edited successfully!"; // Success message
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Pets.Any(e => e.PetID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the pets list
            }
            return View(pet); // Return to form if there were validation errors
        }


        // GET: PetsMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(p => p.PetID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet); // Render the delete confirmation view
        }

        // POST: PetsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pet deleted successfully!"; // Success message
            }

            return RedirectToAction(nameof(Index)); // Redirect to the pets list
        }

    }
}
