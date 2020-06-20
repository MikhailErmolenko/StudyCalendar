using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Models.User;

namespace WebCalendar.DAL.EF.Initializer.Mapper
{
    public class UserProfile : AutoMapperProfile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegisterServiceModel>();
        }
    }
}