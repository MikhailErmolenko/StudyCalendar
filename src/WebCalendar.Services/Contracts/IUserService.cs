using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebCalendar.Services.Models.User;

namespace WebCalendar.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserServiceModel>> GetAllAsync();
        Task<UserServiceModel> GetByIdAsync(Guid id);
        Task UpdateAsync(UserServiceModel entity);
        Task RemoveAsync(Guid id);
        Task RemoveAsync(UserServiceModel entity);

        Task<UserServiceModel> GetByPrincipalAsync(ClaimsPrincipal principal);

        Task<IdentityResult> RegisterAsync(UserRegisterServiceModel userRegisterServiceModel);
        Task<UserTokenServiceModel> AuthenticateAsync(UserAuthenticateServiceModel userAuthenticateServiceModel);
        Task Logout();
        
        Task SubscribeOnEmailNotificationAsync(Guid userId);
        Task UnsubscribeFromEmailNotificationAsync(Guid userId);
        Task<bool> IsSubscribeOnEmailNotificationAsync(Guid userId);
        
        Task SubscribeOnPushNotificationAsync(Guid userId, string token);
        Task UnsubscribeFromPushNotificationAsync(Guid userId, string token);
    }
}