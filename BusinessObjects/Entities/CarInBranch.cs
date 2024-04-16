using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Entities
{
    public class CarInBranch
    {
        public int CarId { set; get; }
        
        public int BranchId { set; get; }

        public Car Car { set; get; }

        public Branch Branch { set; get; }

    }
  
}
