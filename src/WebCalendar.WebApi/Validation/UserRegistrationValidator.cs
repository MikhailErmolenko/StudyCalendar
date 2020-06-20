using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCalendar.Services.Models.User;

namespace WebCalendar.WebApi.Validation
{
    public class UserRegistrationValidator : AbstractValidator<UserRegisterServiceModel>
    {
        private const string INVALID_NAME_MESSAGE = "{PropertyName} must contain only letters and '-'";
        private const int MIN_NAME_LENGTH = 2;
        private const int MAX_NAME_LENGTH = 20;


        public UserRegistrationValidator()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .MinimumLength(MIN_NAME_LENGTH)
                .MaximumLength(MAX_NAME_LENGTH)
                .Must(IsAValidName).WithMessage(INVALID_NAME_MESSAGE)
                .WithName("²ì'ÿ");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .MinimumLength(MIN_NAME_LENGTH)
                .MaximumLength(MAX_NAME_LENGTH)
                .Must(IsAValidName).WithMessage(INVALID_NAME_MESSAGE)
                .WithName("Ôàì³ë³ÿ");

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();
        }

        private bool IsAValidName(string name)
        {
            return name.Replace("-", "").All(Char.IsLetter);
        }
    }
}
