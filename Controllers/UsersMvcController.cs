using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Data;
using PetAdoptionAPI.Models;

namespace PetAdoptionAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class UsersMvcController : Controller
    {
        private readonly PetAdoptionContext _context;

        public UsersMvcController(PetAdoptionContext context)
        {
            _context = context;
        }

        // GET: UsersMvc
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users); // Render a list of users
        }

        // GET: UsersMvc/Details/5
        [HttpGet("{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Render user details
        }

        // GET: UsersMvc/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Name,PhoneNumber,Role,Email")] Users user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User created successfully!"; // Success message
                return RedirectToAction(nameof(Index)); // Redirect to index
            }
            return View(user); // Return the form with validation errors
        }

        // GET: UsersMvc/Edit/5
        [HttpGet("{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Render the edit form
        }

        // POST: UsersMvc/Edit/5
        [HttpPost("{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Name,PhoneNumber,Role,Email")] Users user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "User edited successfully!"; // Success message
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(e => e.UserID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to index
            }
            return View(user); // Return the form if there were validation errors
        }

        // GET: UsersMvc/Delete/5
        [HttpGet("{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Render delete confirmation view
        }

        // POST: UsersMvc/Delete/5
        [HttpPost("{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User deleted successfully!"; // Success message
            }
            return RedirectToAction(nameof(Index)); // Redirect to index
        }
    }
}
