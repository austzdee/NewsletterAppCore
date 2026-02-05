using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Data;
using NewsletterAppCore.Models;
using System.Threading.Tasks;

namespace NewsletterAppCore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var signups = await _context.SignUps.ToListAsync();
            return View(signups);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            return View(signup);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            return View(signup);
        }

        // POST: Admin/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(NewsletterSignUp model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            return View(signup);
        }

        // POST: Admin/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup != null)
            {
                _context.SignUps.Remove(signup);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}