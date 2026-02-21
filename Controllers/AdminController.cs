using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Data;
using NewsletterAppCore.Models;
using NewsletterAppCore.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NewsletterAppCore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly NewsletterDbContext _context;


        public AdminController(NewsletterDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var signups = await _context.SignUps
                .Select(x => new SignUpVm
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailAddress = x.EmailAddress
                })
                .ToListAsync();

            return View(signups);
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            var vm = new SignUpVm
            {
                Id = signup.Id,
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                EmailAddress = signup.EmailAddress
            };

            return View(vm);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            var vm = new SignUpVm
            {
                Id = signup.Id,
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                EmailAddress = signup.EmailAddress
            };

            return View(vm);
        }

        // POST: Admin/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SignUpVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var signup = await _context.SignUps.FindAsync(model.Id);
            if (signup == null)
                return NotFound();

            signup.FirstName = model.FirstName;
            signup.LastName = model.LastName;
            signup.EmailAddress = model.EmailAddress;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var signup = await _context.SignUps.FindAsync(id);
            if (signup == null)
                return NotFound();

            var vm = new SignUpVm
            {
                Id = signup.Id,
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                EmailAddress = signup.EmailAddress
            };

            return View(vm);
        }

        // POST: Admin/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
