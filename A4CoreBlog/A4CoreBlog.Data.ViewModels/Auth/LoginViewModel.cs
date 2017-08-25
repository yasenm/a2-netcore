using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.ViewModels
{
    public class LoginViewModel : IVIewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
