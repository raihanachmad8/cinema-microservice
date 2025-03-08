using FluentValidation;
using TicketService.Application.DTOs.Requests;
using System;

namespace TicketService.Application.Validators
{
    public class TicketRequestValidator : AbstractValidator<TicketRequest>
    {
        public TicketRequestValidator()
        {
            RuleFor(x => x.ScheduleId)
                .NotEmpty().WithMessage("TicketScheduleId is required.");
            RuleFor(x => x.SeatId)
                .NotEmpty().WithMessage("SeatId is required.")
                .Must(BeAValidSeatId).WithMessage("SeatId must be a valid integer greater than 0.");
        }

        private bool BeAValidSeatId(int seatId)
        {
            return seatId > 0; 
        }
    }
}