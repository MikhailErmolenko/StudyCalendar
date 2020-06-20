using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Models.User;

namespace WebCalendar.Services.Mapper
{
    public class UserServiceModelProfile : AutoMapperProfile
    {
        public UserServiceModelProfile()
        {
            CreateMap<User, UserServiceModel>();
            CreateMap<User, UserRegisterServiceModel>();

            CreateMap<UserServiceModel, User>();
            CreateMap<UserRegisterServiceModel, User>()
                .ForMember(u => u.UserName, o => o.MapFrom(u => u.Email));

            CreateMap<User, UserTokenServiceModel>();

            CreateMap<User, UserSummaryModel>();
        }
    }
}