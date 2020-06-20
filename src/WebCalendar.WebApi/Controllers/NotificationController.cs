using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IUserService _userService;

        public NotificationController(IUserService userService)
        {
            _userService = userService;
        }

        //PUT: api/notification/email/subscribe/{userId}
        [HttpPut("email/subscribe/{userId}")]
        public async Task<IActionResult> SubscribeOnEmailNotification(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.SubscribeOnEmailNotificationAsync(user.Id);

            return NoContent();
        }
        
        //PUT: api/notification/email/unsubscribe/{userId}
        [HttpPut("email/unsubscribe/{userId}")]
        public async Task<IActionResult> UnsubscribeFromEmailNotification(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.UnsubscribeFromEmailNotificationAsync(user.Id);

            return NoContent();
        }
        
        //PUT: api/notification/email/isSubscribe/{userId}
        [HttpGet("email/isSubscribe/{userId}")]
        public async Task<ActionResult<bool>> isSubscribeOnEmailNotification(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            bool isSubscribeOnEmailNotification = await _userService.IsSubscribeOnEmailNotificationAsync(user.Id);

            return Ok(isSubscribeOnEmailNotification);
        }
        
        //POST: api/notification/push/subscribe/{userId}
        [HttpPost("push/subscribe/{userId}")]
        public async Task<IActionResult> SubscribeOnPushNotification(Guid userId, 
            [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.SubscribeOnPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }

        //POST: api/notification/push/unsubscribe/{userId}
        [HttpPost("push/unsubscribe/{userId}")]
        public async Task<IActionResult> UnsubscribeFromPushNotification(Guid userId, 
            [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user == null || user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.UnsubscribeFromPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }
    }
}