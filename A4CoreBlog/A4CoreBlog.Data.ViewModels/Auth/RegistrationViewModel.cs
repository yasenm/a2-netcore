using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Profession { get; set; }
        public string AvatarLink { get; set; }
    }
}
