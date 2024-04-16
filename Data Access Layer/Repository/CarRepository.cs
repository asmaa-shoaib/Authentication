using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data_Access_Layer.Repository
{
    public class CarRepository : ICar
    {
        private readonly Data_Base _data_Base;
       // private readonly Data_Base _data_Base;
        public CarRepository(Data_Base data_Base)
        {
            _data_Base = data_Base;
        }
        public IEnumerable<Car> GetAll()
        {
            return _data_Base.Cars.Include(o => o.Details).Include(o => o.Brand).Include(o => o.Photoes).ToList();
        }
        public IEnumerable<Car> GetBestSeller()
        {
            return _data_Base.Cars.Include(o => o.Details).Include(o => o.Brand).Include(o => o.Photoes).Where(o=>o.BestSeller==true).ToList();

        }
        public Car Get(int? Id)
        {
            Car c = _data_Base.Cars.Include(o => o.Details).Include(o => o.Brand).Include(o=>o.Photoes).FirstOrDefault(e => e.ID == Id);
            Car car = _data_Base.Cars.FirstOrDefault(e => e.ID == Id);
            return c;
        }
        public bool ExistCar(int? Id)
        {
            return _data_Base.Cars.Any(e => e.ID == Id);
        }
        public bool Add(Car car)
        {
            _data_Base.Cars.Add(car);
           return Save();
        }
        public bool Delete(int? Id)
        {
            _data_Base.Cars.Remove(Get(Id));
            return Save();
        }
        public bool UpdateCar(Car car)
        {
            _data_Base.Update(car);
            return Save();
        }
        public ICollection<Branch> GetBrancesContainCar(int? CarId)
        {
            ICollection < Branch> BranchContainCar =_data_Base.CarInBranch.Where(e=>e.CarId==CarId).Select(e=>e.Branch).ToList();
            return BranchContainCar ;
        }

        public ICollection<Car> GetCarinBranch(int? BranchId)
        {
            ICollection<Car> CarInBrach = _data_Base.CarInBranch.Where(e => e.BranchId == BranchId).Select(e => e.Car).ToList();

            return CarInBrach;
        }

        

        public ICollection<Photo> getPhotes(int? Id)
        {
            return _data_Base.Photos.Where(e => e.Car.ID == Id).ToList();
            
        }
        public ICollection<Detail> getDetails(int? Id)
        {
            return _data_Base.Details.Where(e => e.Car.ID == Id).ToList();

        }

        public bool Save()
        {
           int save= _data_Base.SaveChanges();
            return save > 0?true:false;
        }

     

        

      
    }
}
