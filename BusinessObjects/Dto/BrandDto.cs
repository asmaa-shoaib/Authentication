
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObjects.Dto
{
    public class BrandDto
    {

        
        public int ID { get; set; }
        
        public string Name { get; set; }
        public string ImageUrl { set; get; }
    }
}
