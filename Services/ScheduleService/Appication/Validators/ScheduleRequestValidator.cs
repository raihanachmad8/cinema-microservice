using FluentValidation;
using ScheduleService.Application.DTOs.Requests;

namespace ScheduleService.Application.Validators
{
    public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
    {
        public ScheduleRequestValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage("MovieId is required.");

            RuleFor(x => x.StudioId)
                .NotEmpty().WithMessage("StudioId is required.");

            RuleFor(x => x.StartDatetime)
                .NotEmpty().WithMessage("StartDatetime is required.");

            RuleFor(x => x.TicketPrice)
                .NotEmpty().WithMessage("TicketPrice is required.")
                .GreaterThan(0).WithMessage("Ticket price must be a positive value.");
        }
    }
}