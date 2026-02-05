using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Models;

namespace NewsletterAppCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NewsletterSignUp> SignUps { get; set; }
    }
}