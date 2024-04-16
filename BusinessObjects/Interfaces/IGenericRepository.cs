using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int? entryId);
        bool Add(T entry);
        bool Update(int? editedID, T edited);
        bool Delete(int? deletedID);
        bool Save();
        
    }
}
