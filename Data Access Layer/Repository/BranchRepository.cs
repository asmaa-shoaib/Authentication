using BusinessObjects.Entities;
using BusinessObjects.Interfaces;


namespace Data_Access_Layer.Repository
{
    public class BranchRepository : IBranch
    {
        private readonly Data_Base _data_Base;
        public BranchRepository(Data_Base data_Base)
        {
             
            _data_Base = data_Base;

        }
        public bool Add(Branch branch)
        {
            _data_Base.Add(branch);
            return Save();
           
        }

        public bool Delete(Branch branch)
        {
            
            _data_Base.Branches.Remove(branch);
            return Save();

        }

        public bool ExistBranch(int? Id)
        {
            return _data_Base.Branches.Any(e=>e.Id==Id);
            
        }

        public Branch Get(int? Id)
        {
            return _data_Base.Branches.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Branch> GetAll()
        {
            return _data_Base.Branches.ToList();
        }

        public ICollection<Branch> GetBrancesContainCar(int? CarId)
        {
            ICollection < Branch > branches= _data_Base.CarInBranch.Where(e=>e.CarId==CarId).Select(e=>e.Branch).ToList();
           return branches;
        }

        public ICollection<Car> GetCarinBranch(int? BranchId)
        {
            ICollection<Car> cars = _data_Base.CarInBranch.Where(e => e.BranchId == BranchId).Select(e => e.Car).ToList();
            return cars;
        }

        public bool Save()
        {
            int save = _data_Base.SaveChanges();
            return save>0?true:false;
        }

        public bool UpdateBranch( Branch branch)
        {
            _data_Base.Update(branch);
            return Save();
        }
    }
}
