using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Interfaces
{
    public interface IDetail
    {
        IEnumerable<Detail> GetAll();

        Detail Get(int? Id);

        bool ExistDetail(int? Id);

        bool Add(Detail Detail);

        bool Delete(Detail Detail);

        bool UpdateDetail(Detail Detail);

        Detail GetCarDetail(int? CaeId);

        bool Save();


    }
}
