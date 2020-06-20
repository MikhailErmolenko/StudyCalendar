using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Calendar;
using WebCalendar.Services.Models.User;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly ICalendarService _calendarService;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, 
            IConfiguration config, IUnitOfWork uow, ICalendarService calendarService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _uow = uow;
            _calendarService = calendarService;
        }

        public Task<IEnumerable<UserServiceModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserServiceModel> GetByIdAsync(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());

            UserServiceModel userServiceModel = _mapper.Map<User, UserServiceModel>(user);

            return userServiceModel;
        }

        public async Task UpdateAsync(UserServiceModel entity)
        {
            if (entity == null)
            {
                return;
            }

            User user = _mapper.Map<UserServiceModel, User>(entity);

            _uow.GetRepository<User>().Update(user);

            await _uow.SaveChangesAsync();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(UserServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserServiceModel> GetByPrincipalAsync(ClaimsPrincipal principal)
        {
            string userId = _userManager.GetUserId(principal);
            
            User user = await _userManager.FindByNameAsync(userId);

            UserServiceModel userServiceModel = _mapper.Map<User, UserServiceModel>(user);

            return userServiceModel;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterServiceModel userRegisterServiceModel)
        {
            
            
            User user = _mapper.Map<UserRegisterServiceModel, User>(userRegisterServiceModel);

            bool exists = await _uow.GetRepository<User>().ExistsAsync(u => u.Email == user.Email);
            if (exists)
            {
                return IdentityResult.Failed(new IdentityError {Code = "DuplicateEmail"});
            }

            IdentityResult result = await _userManager.CreateAsync(user, userRegisterServiceModel.Password);

            //create default calendar
            await _calendarService.AddAsync(new CalendarCreationServiceModel
            {
                Title = "Default",
                Description = "",
                UserId = user.Id
            });

            return result;
        }

        public async Task<UserTokenServiceModel> AuthenticateAsync(UserAuthenticateServiceModel userAuthenticateServiceModel)
        {
            User user = await _userManager.FindByEmailAsync(userAuthenticateServiceModel.Email);

            UserTokenServiceModel userTokenServiceModel = _mapper.Map<User, UserTokenServiceModel>(user);

            SignInResult result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                userAuthenticateServiceModel.Password,
                true,
                false
            );

            string token = GenerateJsonWebToken(user);
            userTokenServiceModel.Token = token;
            
            return userTokenServiceModel;
        }
        
        private string GenerateJsonWebToken(User user)
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],  
                _config["Jwt:Audience"],  
                new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName), 
                },  
                expires: DateTime.Now.AddMinutes(Double.Parse(_config["Jwt:Lifetime"])),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }
        
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        
        public async Task SubscribeOnEmailNotificationAsync(Guid userId)
        {
            User user = await _uow.GetRepository<User>().GetByIdAsync(userId);

            user.IsSubscribedToEmailNotifications = true;

            _uow.GetRepository<User>().Update(user);

            await _uow.SaveChangesAsync();
        }

        public async Task UnsubscribeFromEmailNotificationAsync(Guid userId)
        {
            User user = await _uow.GetRepository<User>().GetByIdAsync(userId);

            user.IsSubscribedToEmailNotifications = false;

            _uow.GetRepository<User>().Update(user);

            await _uow.SaveChangesAsync();
        }

        public async Task<bool> IsSubscribeOnEmailNotificationAsync(Guid userId)
        {
            bool isSubscribe = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                selector: u => u.IsSubscribedToEmailNotifications,
                predicate: u => u.Id == userId);

            return isSubscribe;
        }

        public async Task SubscribeOnPushNotificationAsync(Guid userId, string token)
        {
            var pushSubscription = new PushSubscription
            {
                DeviceToken = token,
                UserId = userId
            };

            await _uow.GetRepository<PushSubscription>().AddAsync(pushSubscription);

            await _uow.SaveChangesAsync();
        }

        public async Task UnsubscribeFromPushNotificationAsync(Guid userId, string token)
        {
            IEnumerable<PushSubscription> pushSubscriptions = await _uow.GetRepository<PushSubscription>().GetAllAsync(
                p => p.UserId == userId && p.DeviceToken == token);

            foreach (PushSubscription pushSubscription in pushSubscriptions)
            {
                _uow.GetRepository<PushSubscription>().Remove(new PushSubscription
                {
                    Id = pushSubscription.Id
                });
            }

            await _uow.SaveChangesAsync();
        }
    }
}