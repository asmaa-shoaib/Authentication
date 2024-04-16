using BusinessObjects.Entities;


namespace BusinessObjects.Interfaces
{
    public interface IBranch
    {
        IEnumerable<Branch> GetAll();

        Branch Get(int? Id);
         
        bool ExistBranch(int? Id);

        bool Add(Branch branch);

        bool Delete(Branch branch);

        bool UpdateBranch( Branch branch);

        ICollection<Branch> GetBrancesContainCar(int? CarId);
        ICollection<Car> GetCarinBranch(int? BranchId);

        bool Save();


    }
}
