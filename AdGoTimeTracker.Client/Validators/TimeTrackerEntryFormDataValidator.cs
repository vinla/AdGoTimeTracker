using AdGoTimeTracker.Client.Models;
using FluentValidation;

namespace AdGoTimeTracker.Client.Validators;

public class TimeTrackerEntryFormDataValidator : AbstractValidator<TimeTrackerEntryFormData>
{
    public TimeTrackerEntryFormDataValidator()
    {
        RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("You must provide a description for the task")
            .Length(3, 255).WithMessage("Add a description for the task between 3 and 255 characters long");

        RuleFor(x => x.StartTime)
            .LessThan(x => x.EndTime).WithMessage("Set a valid start time, it must come before End");            

        RuleFor(x => x.EndTime)                
            .GreaterThan(x => x.StartTime).WithMessage("Set a valid end time, it must come after Start");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TimeTrackerEntryFormData>.CreateWithOptions((TimeTrackerEntryFormData)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}