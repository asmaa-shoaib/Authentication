using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class DetailRepository : IDetail
    {
        private readonly Data_Base _data_Base;

        public DetailRepository(Data_Base data_Base)
        {
            _data_Base = data_Base;
        }
        public bool Add(Detail Detail)
        {
            _data_Base.Add(Detail);
            return Save();
        }

        public bool Delete(Detail Detail)
        {
            _data_Base.Remove(Detail);
            return Save();
        }

        public bool ExistDetail(int? Id)
        {

            return _data_Base.Details.Any(e => e.Id == Id);
        }

        public Detail Get(int? Id)
        {
            return _data_Base.Details.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Detail> GetAll()
        {
            return _data_Base.Details.ToList();
        }

        public Detail GetCarDetail(int? CaeId)
        {
            Detail detail = _data_Base.Details.FirstOrDefault(e => e.Car.ID == CaeId);

            return detail ;
        }

        public bool UpdateDetail(Detail Detail)
        {
            _data_Base.Details.Update(Detail);
            return Save();
        }
        public bool Save()
        {
            int save = _data_Base.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
