using System.ComponentModel.DataAnnotations;

namespace NewsletterAppCore.Models
{
    public class NewsletterSignUp
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]

        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(255)]
        public string EmailAddress { get; set; }



        [StringLength(100)]
        public string? SocialSecurityNumber { get; set; } // This field can be null in the database
    }
}