using API.DTO;
using API.Entities;
using AutoMapper;

using API.Extensions;
using System.Linq;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDTO>()
              .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
              .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DayOfBirth.CalculateAge()));;
            CreateMap<Photo,PhotoDTO>();
             CreateMap<MemberUpdateDTO,AppUser>();
        }
    }
}