using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Export.Contracts;
using WebCalendar.Services.Models.Calendar;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models.Calendar;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;

        public CalendarController(ICalendarService calendarService, IMapper mapper, IUserService userService, IExportService exportService)
        {
            _calendarService = calendarService;
            _mapper = mapper;
            _userService = userService;
            _exportService = exportService;
        }

        [HttpGet("export-calendar/{calendarId}")]
        public async Task<IActionResult> ExportCalendar(Guid userId, Guid calendarId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            byte[] calendarBytes = await _exportService.ExportCalendar(calendarId);
            string fileType = "text/calendar";
            string fileName = "web-calendar.ics";

            return File(calendarBytes, fileType, fileName);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<CalendarInfoModel>> GetUserCalendars(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }
            
            IEnumerable<CalendarServiceModel> userCalendarsServiceModels = await _calendarService
                .GetAllUserCalendarsAsync(user.Id);

            IEnumerable<CalendarInfoModel> userCalendars = _mapper
                .Map<IEnumerable<CalendarServiceModel>, IEnumerable<CalendarInfoModel>>(userCalendarsServiceModels);

            return Ok(userCalendars);
        }
    }
}