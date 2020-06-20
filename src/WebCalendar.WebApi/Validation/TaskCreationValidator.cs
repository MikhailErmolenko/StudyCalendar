using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Validation
{
    public class TaskCreationValidator : AbstractValidator<TaskCreationModel>
    {
        private const int MIN_TITLE_LENGTH = 2;
        private const int MAX_TITLE_LENGTH = 20;
        private const int MAX_DESCRIPTION_LENGHT = 200;

        public TaskCreationValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty()
                .MinimumLength(MIN_TITLE_LENGTH)
                .MaximumLength(MAX_TITLE_LENGTH);

            RuleFor(t => t.Description)
                .MaximumLength(MAX_DESCRIPTION_LENGHT);
        }
    }
}
