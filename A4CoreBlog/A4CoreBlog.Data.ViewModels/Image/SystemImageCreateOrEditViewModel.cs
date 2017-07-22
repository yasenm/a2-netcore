using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace A4CoreBlog.Data.ViewModels
{
    public class SystemImageCreateOrEditViewModel
    {
        public int? Id { get; set; }
        public byte[] Content { get; set; }
        public string Extension { get; set; }

        public string Title { get; set; }
        public string Alt { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }

        public IFormFile ImageRawContent { get; set; }

        //[Required(ErrorMessage = "Posted picture is required!")]
        //public HttpPostedFileBase ImageBase { get; set; }
    }
}
