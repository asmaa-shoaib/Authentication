
using BusinessObjects.Entities;

namespace BusinessObjects.Interfaces
{
    public interface ICar
    {
        IEnumerable<Car> GetAll();
        IEnumerable<Car> GetBestSeller();

        Car Get(int? Id);

        bool ExistCar(int? Id);

        bool Add(Car car);

        bool Delete(int? Id);

        bool UpdateCar( Car car);

        ICollection<Branch> GetBrancesContainCar(int? CarId);


        ICollection<Car> GetCarinBranch(int? BranchId);


        ICollection<Photo> getPhotes(int? Id);
        ICollection<Detail> getDetails(int? Id);

        bool Save();
       

    }
}
