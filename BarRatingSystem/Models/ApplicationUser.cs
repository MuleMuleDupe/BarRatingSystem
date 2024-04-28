using Microsoft.AspNetCore.Identity;

namespace BarRatingSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<BarReview> Reviews { get; set; }
    }
}
