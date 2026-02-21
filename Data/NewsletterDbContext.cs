using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Models;

namespace NewsletterAppCore.Data
{
    public class NewsletterDbContext : DbContext
    {
        public NewsletterDbContext(DbContextOptions<NewsletterDbContext> options)
            : base(options)
        {
        }

        public DbSet<NewsletterSignUp> SignUps => Set<NewsletterSignUp>();
    }
}