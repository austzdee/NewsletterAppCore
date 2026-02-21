using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Data;
using NewsletterAppCore.Models;
using NewsletterAppCore.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace NewsletterAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsletterDbContext _context;

        public HomeController(NewsletterDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(NewsletterSignUp model)
        {
            // Trigger model validation (Required, EmailAddress, etc.)
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            _context.SignUps.Add(model);
            await _context.SaveChangesAsync();

            return View("Success");
        }

        public async Task<IActionResult> Admin()
        {
            var signupVms = await _context.SignUps
                .Select(s => new SignUpVm
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EmailAddress = s.EmailAddress,
                    SocialSecurityNumber = s.SocialSecurityNumber
                })
                .ToListAsync();

            return View(signupVms);
        }
    }
}