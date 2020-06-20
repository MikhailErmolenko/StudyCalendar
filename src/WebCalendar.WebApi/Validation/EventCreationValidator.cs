using FluentValidation;
using WebCalendar.WebApi.Models.Event;

namespace WebCalendar.WebApi.Validation
{
    public class EventCreationValidator : AbstractValidator<EventCreationModel>
    {
        private const int MIN_TITLE_LENGTH = 2;
        private const int MAX_TITLE_LENGTH = 20;
        private const int MAX_DESCRIPTION_LENGHT = 200;

        public EventCreationValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty()
                .MinimumLength(MIN_TITLE_LENGTH)
                .MaximumLength(MAX_TITLE_LENGTH);

            RuleFor(e => e.Description)
                .MaximumLength(MAX_DESCRIPTION_LENGHT);

            RuleFor(e => e.DaysOfMounth)
                .ForEach(dm => dm.InclusiveBetween(1, 31));

            RuleFor(e => e.DaysOfWeek)
                .ForEach(dw => dw.InclusiveBetween(1, 7));

            RuleFor(e => e.Monthes)
                .ForEach(m => m.InclusiveBetween(1, 12));

            RuleFor(e => e.Years)
                .ForEach(y => y.InclusiveBetween(1970, 2099));
        }
    }
}
