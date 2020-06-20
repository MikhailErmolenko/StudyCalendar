using WebCalendar.Common;
using WebCalendar.Services.Models.Task;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Mapper
{
    public class TaskModelProfile : AutoMapperProfile
    {
        public TaskModelProfile()
        {
            CreateMap<TaskCreationModel, TaskCreationServiceModel>();
            CreateMap<TaskServiceModel, TaskModel>();
            CreateMap<TaskServiceModel, TaskEditionServiceModel>();
        }
    }
}