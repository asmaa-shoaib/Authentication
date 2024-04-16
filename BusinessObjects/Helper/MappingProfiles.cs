using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Entities;

namespace BusinessObjects.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Car,CarDto>();
            CreateMap<CarDto, Car>();

            CreateMap<Detail, DetailDto>();
            CreateMap<DetailDto, Detail>();

            CreateMap<Branch, BranchDto>();
            CreateMap<BranchDto, Branch>();

            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();
        }
    }
}
