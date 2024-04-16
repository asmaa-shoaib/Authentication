
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities
{
    public class Brand
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string ImageUrl { set; get; }
        [JsonIgnore]
        public ICollection<Car>? cars { get; set; }
    }
}
