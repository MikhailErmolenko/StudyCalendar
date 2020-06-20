using System;
using System.Text;
using System.Threading.Tasks;
using Ical.Net.CalendarComponents;
using Ical.Net.Serialization;
using Microsoft.EntityFrameworkCore;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Export.Contracts;

namespace WebCalendar.Services.Export.Implementation
{
    public class ExportService : IExportService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExportService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<byte[]> ExportCalendar(Guid id)
        {
            Calendar calendar = await _uow.GetRepository<Calendar>()
             .GetFirstOrDefaultAsync(
                 predicate: c => c.Id == id,
                 include: source => source
                    .Include(c => c.Events)
                    .Include(c => c.Tasks));

            Ical.Net.Calendar iCalendar = _mapper.Map<Calendar, Ical.Net.Calendar>(calendar);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            string serializedCalendar = serializer.SerializeToString(iCalendar);
            byte[] bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);

            return bytesCalendar;
        }

        public async Task<byte[]> ExportEvent(Guid id)
        {
            Event @event = await _uow.GetRepository<Event>().GetByIdAsync(id);

            CalendarEvent iCalendar = _mapper.Map<Event, CalendarEvent>(@event);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            string serializedCalendar = serializer.SerializeToString(iCalendar);
            byte[] bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);

            return bytesCalendar;
        }
    }
}
