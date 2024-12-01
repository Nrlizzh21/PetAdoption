using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using PetAdoptionAPI.Models;

namespace PetAdoptionAPI.Controllers
{
    public class AdoptionsMvcController : Controller
    {
        private readonly PetAdoptionContext _context;

        public AdoptionsMvcController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: AdoptionsMvc
        public async Task<IActionResult> Index()
        {
            var adoptions = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .ToListAsync();
            return View(adoptions); // Return the list of adoptions
        }

        // GET: AdoptionsMvc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.AdoptionID == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption); // Return adoption details view
        }

        // GET: AdoptionsMvc/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserID"] = new SelectList(await _context.Users.ToListAsync(), "UserID", "Name");
            var availablePets = await _context.Pets
                .Where(p => p.AdoptionStatus == "Available")
                .ToListAsync();
            ViewData["PetID"] = new SelectList(availablePets, "PetID", "Name");
            return View(); // Render the Create form with users and only available pets
        }

        // POST: AdoptionsMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetID,UserID,Status")] Adoptions adoption)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(adoption.UserID);
                var pet = await _context.Pets.FindAsync(adoption.PetID);

                if (user == null || pet == null)
                {
                    return BadRequest("Invalid UserID or PetID.");
                }

                adoption.UserName = user.Name;
                adoption.PetName = pet.Name;
                adoption.RequestDate = DateTime.UtcNow;

                _context.Add(adoption);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Adoption created successfully!"; // Success message

                return RedirectToAction(nameof(Index));
            }

            ViewData["UserID"] = new SelectList(await _context.Users.ToListAsync(), "UserID", "Name", adoption.UserID);
            ViewData["PetID"] = new SelectList(await _context.Pets.ToListAsync(), "PetID", "Name", adoption.PetID);
            return View(adoption); // Return to the form if there are validation errors
        }

        // GET: AdoptionsMvc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption); // Render the Edit form for the adoption
        }

        // POST: AdoptionsMvc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdoptionID,PetID,UserID,Status")] Adoptions adoption)
        {
            if (id != adoption.AdoptionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAdoption = await _context.Adoptions
                        .Include(a => a.Pet)
                        .FirstOrDefaultAsync(a => a.AdoptionID == id);

                    if (existingAdoption == null)
                    {
                        return NotFound();
                    }

                    existingAdoption.Status = adoption.Status;

                    if (adoption.Status == "Approved")
                    {
                        var petToUpdate = existingAdoption.Pet;
                        if (petToUpdate != null)
                        {
                            petToUpdate.AdoptionStatus = "Adopted";
                            _context.Update(petToUpdate);
                        }
                    }

                    _context.Update(existingAdoption);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Adoption updated successfully!"; // Success message
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Adoptions.Any(e => e.AdoptionID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(adoption);
        }

        // GET: AdoptionsMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoptions
                .Include(a => a.User)
                .Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.AdoptionID == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: AdoptionsMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoptions.FindAsync(id);
            if (adoption != null)
            {
                _context.Adoptions.Remove(adoption);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Adoption deleted successfully!"; // Success message
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApproveAdoption(int adoptionId)
        {
            var adoption = await _context.Adoptions.Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.AdoptionID == adoptionId);

            if (adoption != null && adoption.Status != "Approved" && adoption.Pet.AdoptionStatus != "Adopted")
            {
                adoption.Status = "Approved";
                var pet = adoption.Pet;
                if (pet != null)
                {
                    pet.AdoptionStatus = "Adopted";
                    _context.Update(pet);
                }

                _context.Update(adoption);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Adoption approved successfully!"; // Success message
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RejectAdoption(int adoptionId)
        {
            var adoption = await _context.Adoptions.Include(a => a.Pet)
                .FirstOrDefaultAsync(a => a.AdoptionID == adoptionId);

            if (adoption != null && adoption.Status != "Rejected")
            {
                adoption.Status = "Rejected";

                var pet = adoption.Pet;
                if (pet != null)
                {
                    var otherApprovedAdoption = await _context.Adoptions
                        .AnyAsync(a => a.PetID == pet.PetID && a.Status == "Approved");

                    if (!otherApprovedAdoption)
                    {
                        pet.AdoptionStatus = "Available";
                        _context.Update(pet);
                    }
                }

                _context.Update(adoption);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Adoption rejected successfully!"; // Success message
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
