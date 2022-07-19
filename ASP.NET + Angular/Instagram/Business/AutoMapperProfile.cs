using AutoMapper;
using Business.Models;

namespace Business
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Entities.Post, PostViewModel>().ReverseMap();
            CreateMap<Data.Entities.Profile, ProfileViewModel>().ReverseMap();
        }
    }
}
