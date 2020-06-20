using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Event;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models.Event;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper, IUserService userService)
        {
            _eventService = eventService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<EventModel>> CreateEvent(Guid userId,
            [FromBody] EventCreationModel eventCreationModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            EventCreationServiceModel eventCreationServiceModel = _mapper
                .Map<EventCreationModel, EventCreationServiceModel>(eventCreationModel);
            EventServiceModel eventServiceModel = await _eventService.AddAsync(eventCreationServiceModel);

            EventModel eventModel = _mapper.Map<EventServiceModel, EventModel>(eventServiceModel);

            return CreatedAtAction(nameof(GetEvent), new
            {
                eventId = eventModel.Id,
                userId = user.Id
            }, eventModel);
        }

        [HttpGet("{eventId}/{userId}")]
        public async Task<ActionResult<EventModel>> GetEvent(Guid eventId, Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            EventServiceModel eventServiceModel = await _eventService.GetByIdAsync(eventId);

            EventModel eventModel = _mapper.Map<EventServiceModel, EventModel>(eventServiceModel);

            return Ok(eventModel);
        }
    }
}