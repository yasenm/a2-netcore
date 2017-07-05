using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.ViewModels
{
    public abstract class DescribableViewModel
    {
        [Required]
        [StringLength(5000, MinimumLength = 4)]
        public string Description { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Summary { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Title { get; set; }
    }
}
