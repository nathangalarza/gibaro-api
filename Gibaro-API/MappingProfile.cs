using AutoMapper;
using Entities;
using Entities.Models;
using Converters;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.User;

//{{Using}}//


namespace Gibaro_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreationDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            
            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();

          
            //{{Mapper}}//
        }
    }
}












