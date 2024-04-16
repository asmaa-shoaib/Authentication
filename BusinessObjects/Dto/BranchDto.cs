
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dto
{
    public class BranchDto
    {
       
        public int Id { set; get; }

        public string Governoment { set; get; }
        public string Name { set; get; }
        public string Location { set; get; }
        public double lat { set; get; }
        public double lng { set; get; }
        public int Mobile { set; get; }
        public int Phone1 { set; get; }
        public int Phone2 { set; get; }
    }
  
}
