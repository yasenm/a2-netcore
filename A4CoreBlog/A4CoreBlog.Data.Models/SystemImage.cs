using A4CoreBlog.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace A4CoreBlog.Data.Models
{
    public class SystemImage : DeletableEntity
    {
        [Key]
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        
        public string Title { get; set; }
        public string Alt { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
    }
}
