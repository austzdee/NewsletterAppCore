using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Data;
using NewsletterAppCore.Models;
using NewsletterAppCore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsletterAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string FirstName, string LastName, string EmailAddress)
        {
            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(EmailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            var signup = new NewsletterSignUp
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress
            };

            _context.SignUps.Add(signup);
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