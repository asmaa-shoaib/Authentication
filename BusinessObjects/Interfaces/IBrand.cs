using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Interfaces
{
    public interface IBrand
    {
        IEnumerable<Brand> GetAll();
        Brand Get(int? Id);
        bool BrandExist(int? id);
        bool AddBrand(Brand brand);
        bool UpdateBrand( Brand _Brand);

        bool DeleteBrand(Brand brand);
        bool DeleteImage(string imageFileName);
        IEnumerable<Car> GetCarInBrand(int? id);

    }
}
