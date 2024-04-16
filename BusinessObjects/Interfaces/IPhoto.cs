using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;

namespace BusinessObjects.Interfaces
{
    public interface IPhoto
    {
        bool DeleteImage(string imageFileName);
        bool Add(Photo Photo);
        bool Delete(Photo Photo);

        bool ExistPhoto(int? Id);

        Photo Get(int? Id);

        IEnumerable<Photo> GetAll();

        IEnumerable<Photo> GetCarPhotos(int? CaeId);

        bool UpdatePhoto(Photo Photo);

        bool Save();
    }
}
