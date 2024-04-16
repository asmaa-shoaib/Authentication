using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Authentication
{
    public class RegisterMoldel
    {
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
   
}
