using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Authentication
{
    public class ApplicationUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
    }
   
}
