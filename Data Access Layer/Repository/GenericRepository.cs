using BusinessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data_Access_Layer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Data_Base _data_Base;
        public DbSet<T> Table;

        public GenericRepository(Data_Base data_Base)
        {
            _data_Base = data_Base;
            Table = _data_Base.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return Table.ToList();
        }
       
        public T Get(int? entryId)
        {
            return Table.Find(entryId);
        }

        public bool Add(T entry)
        {
            Table.Add(entry);
            return Save();
        }
        public bool Update(int? editedID, T edited)
        {
            if (Get(editedID) == null) return false;
            Table.Update(edited);
            return Save();
        }

        public bool Delete(int? deletedID)
        {
            if (Get(deletedID) == null) return false;
            Table.Remove(Get(deletedID));
            return Save();
        }

        public bool Save()
        {
            int save = _data_Base.SaveChanges();
            return save > 0 ? true : false;

        }




    }
}
