using AutoMapper;

namespace WebCalendar.Common
{
    public class WebCalendarAutoMapper : Contracts.IMapper
    {
        private readonly IMapper _mapper;

        public WebCalendarAutoMapper()
        {
            _mapper = AutoMapperConfiguration.Configure();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}