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
    public class UpdateEvent : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/Event", async (IMediator mediator, UpdateEventCommand command) =>
            {
                return await mediator.Send(command);
            })
            .WithName(nameof(UpdateEvent))
            .WithTags(nameof(Event))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        }

        public class UpdateEventCommand : IRequest<IResult>
        {
            public int EventId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime StartDate { get; set; } 
            public DateTime EndDate { get; set; }
            public string Location { get; set; } = string.Empty;
            public string EventCreatedBy { get; set; } = string.Empty;
        }

        public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<UpdateEventCommand> _validator;

            public UpdateEventHandler(EventTicketingContext context, IValidator<UpdateEventCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var @event = await _context.Events.FindAsync(request.EventId);
                if (@event is null)
                    return Results.NotFound($"No se encuentra ningun evento de id {request.EventId}");

                @event.UpdateEvent(request);

                _context.Update(@event);
                await _context.SaveChangesAsync();

                return Results.NoContent();
            }
        }

        public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
        {
            public UpdateEventValidator()
            {
                RuleFor(x => x.EventId).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.StartDate).NotEmpty();
                RuleFor(x => x.EndDate).NotEmpty();
                RuleFor(x => x.Location).NotEmpty();
                RuleFor(x => x.EventCreatedBy).NotEmpty();
            }
        }
    }
}
