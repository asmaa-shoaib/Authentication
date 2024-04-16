
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities
{
    public class Photo
    {
        [Key]
        public int Id { set; get; }
        public string Url { set; get; }
        //[NotMapped]
        //public IFormFile? ImageFile { set; get; }

        [ForeignKey("Car")]
        [Required]
        public int CarId { set; get; }
        [JsonIgnore]
        public virtual Car Car { set; get; }

    }
}
