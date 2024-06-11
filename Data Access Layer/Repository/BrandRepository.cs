using Azure.Core;
using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Data_Access_Layer.Repository
{
    public class BrandRepository:IBrand
    {
        private readonly Data_Base _data_Base;

        private IWebHostEnvironment _environment;
        public BrandRepository(Data_Base data_Base, IWebHostEnvironment environment)
        {
            _data_Base = data_Base;
            _environment = environment;
        }

        public IEnumerable<Brand> GetAll()
        {
            return _data_Base.Brands.ToList();
        }

        public Brand Get(int? Id)
        {
            Brand brand = null;
            if (BrandExist(Id))
            {
                brand= _data_Base.Brands.FirstOrDefault(e => e.ID == Id);
            }
            return brand;
        }
        public bool BrandExist(int? id)
        {
            return _data_Base.Brands.Any(e => e.ID == id);
           
        }
        
        public bool AddBrand(Brand brand)
        {
            _data_Base.Brands.Add(brand);

            return Save();

        }

       public bool UpdateBrand(Brand _Brand)
        {            
            _data_Base.Brands.Update(_Brand);
            return Save() ;
        }

        public bool DeleteBrand(Brand brand)
        {
            DeleteImage(brand.ImageUrl);
            _data_Base.Brands.Remove(brand);
            return Save();
        }
       
        public bool Save()
        {
            int save =_data_Base.SaveChanges();
            return save > 0 ? true : false ;
        }

        public IEnumerable<Car> GetCarInBrand(int? id)
        {
            IEnumerable<Car> CarsInBrand=_data_Base.Cars.Where(o=>o.Brand.ID== id);
           return CarsInBrand;
        }
        public bool DeleteImage(string imageFileName)
        {
            var wwwPath = this._environment.WebRootPath;
            var path = Path.Combine("D:\\Eleman\\new2\\Authentication\\auth\\", "wwwroot\\Uploads\\Brands\\", imageFileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
