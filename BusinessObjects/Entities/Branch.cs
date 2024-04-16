
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Entities
{
    public class Branch
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public string Governoment { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Location { set; get; }

        public double lat { set; get; }
        public double lng { set; get; }
        public int Mobile { set; get; }
        public int Phone1 { set; get; }
        public int? Phone2 { set; get; }
        [JsonIgnore]
        public ICollection<CarInBranch>? CarInBranches { set; get; } 
    }
  
}
