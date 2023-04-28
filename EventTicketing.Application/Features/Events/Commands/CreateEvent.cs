using Carter;
using Carter.ModelBinding;
using EventTicketing.Application.Domain.Entities;
using EventTicketing.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventTicketing.Application.Features.Events.Commands
{
    public class CreateEvent : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/Event", async (IMediator mediator, CreateEventCommand command) =>
            {
                return await mediator.Send(command);
            })
            .WithName(nameof(CreateEvent))
            .WithTags(nameof(Event))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status200OK);
        }
        public class CreateEventCommand : IRequest<IResult>
        {
            public string Name { get; set; } = string.Empty;
            public DateTime StartDate { get; set; } 
            public DateTime EndDate { get; set; }
            public string Location { get; set; } = string.Empty;
            public string EventCreatedBy { get; set; } = string.Empty;
        }

        public class CreateEventHandler : IRequestHandler<CreateEventCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<CreateEventCommand> _validator;

            public CreateEventHandler(EventTicketingContext context, IValidator<CreateEventCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(CreateEventCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var newEvent = new Event(0, request.Name, request.StartDate, request.EndDate, request.Location, request.EventCreatedBy);

                _context.Add(newEvent);
                await _context.SaveChangesAsync();

                return Results.Created($"api/Event/{newEvent.EventId}", null);
            }
        }

        public class CreateEventValidator : AbstractValidator<CreateEventCommand>
        {
            public CreateEventValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
                RuleFor(x => x.Location).NotEmpty();
                RuleFor(x => x.EventCreatedBy).NotEmpty();
            }
        }
    }
}
