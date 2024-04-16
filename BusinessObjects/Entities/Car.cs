
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities
{
    public class Car
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Price { get; set; }

        public bool BestSeller { get; set; }
        [ForeignKey("Brand")]
        [Required]
        public int BrandId { set; get; }

        public Brand? Brand { get; set; }
        
        public Detail Details { get; set; }
        
        public ICollection<Photo> Photoes { get; set; }
        [JsonIgnore]
        public ICollection<CarInBranch> CarInBranches { get; set; }

    }
}
