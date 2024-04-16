
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Dto
{
    public class CarDto
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
        public int BrandId { set; get; }


    }
}
