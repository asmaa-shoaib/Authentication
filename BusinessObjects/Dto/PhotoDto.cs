
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Dto
{
    public class PhotoDto
    {
        [Key]
        public int Id { set; get; }
        public string  Url { set; get; }

       // public IFormFile? ImageFile { set; get; }

        public int CarId { set; get; }

    }
}
