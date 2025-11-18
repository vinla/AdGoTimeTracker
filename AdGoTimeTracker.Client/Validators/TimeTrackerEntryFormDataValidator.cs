using AdGoTimeTracker.Client.Models;
using FluentValidation;

namespace AdGoTimeTracker.Client.Validators
{
    public class TimeTrackerEntryFormDataValidator : AbstractValidator<TimeTrackerEntryFormData>
    {
        public TimeTrackerEntryFormDataValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(3, 255)
                .WithMessage("Add a description for the task between 3 and 255 characters long");

            RuleFor(x => x.StartTime).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Set the start time")
                .LessThan(x => x.EndTime).WithMessage("Start must come before End");            

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Set the end time")
                .GreaterThan(x => x.StartTime).WithMessage("End must come after Start");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<TimeTrackerEntryFormData>.CreateWithOptions((TimeTrackerEntryFormData)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}