using BusinessObjects.Entities;
using BusinessObjects.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Data_Access_Layer.Repository
{
    public class PhotoRepository:IPhoto
    {
        private readonly Data_Base _data_Base;
        private IWebHostEnvironment _environment;
        public PhotoRepository(Data_Base data_Base, IWebHostEnvironment environment)
        {
            _data_Base = data_Base;
            _environment = environment;
        }
        public bool Add(Photo Photo )
        {
            _data_Base.Add(Photo);
            return Save();
        }
        public bool DeleteImage(string imageFileName)
        {
            var wwwPath = this._environment.WebRootPath;
            var path = Path.Combine("D:\\Eleman\\new2\\Authentication\\auth\\", "Uploads\\",imageFileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }

        public bool Delete(Photo Photo)
        {
            DeleteImage(Photo.Url);
            _data_Base.Photos.Remove(Photo);
            return Save();
        }

        public bool ExistPhoto(int? Id)
        {

            return _data_Base.Photos.Any(e => e.Id == Id);
        }

        public Photo Get(int? Id)
        {
            return _data_Base.Photos.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Photo> GetAll()
        {
            return _data_Base.Photos.ToList();
        }

        public IEnumerable<Photo> GetCarPhotos(int? CaeId)
        {
            IEnumerable<Photo> Photos = _data_Base.Photos.Where(e => e.Car.ID == CaeId).ToList();

            return Photos;
        }

        public bool UpdatePhoto(Photo Photo)
        {
            _data_Base.Photos.Update(Photo);
            return Save();
        }
        public bool Save()
        {
            int save = _data_Base.SaveChanges();
            return save > 0 ? true : false;
        }

        
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       