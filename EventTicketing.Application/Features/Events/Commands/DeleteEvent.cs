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
    public class DeleteEvent : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/Event/{eventId}", async (IMediator mediator, int eventId) =>
            {
                return await mediator.Send(new DeleteEventCommand() { EventId = eventId });
            })
            .WithName(nameof(DeleteEvent))
            .WithTags(nameof(Event))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        }

        public class DeleteEventCommand : IRequest<IResult>
        {
            public int EventId { get; set; }
        }

        public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<DeleteEventCommand> _validator;

            public DeleteEventHandler(EventTicketingContext context, IValidator<DeleteEventCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var @event = await _context.Events.FindAsync(request.EventId);
                if (@event is null)
                    return Results.NotFound($"No se encuentra ningun evento de id {request.EventId}");

                _context.Remove(@event);
                await _context.SaveChangesAsync();

                return Results.NoContent();
            }
        }

        public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
        {
            public DeleteEventValidator()
            {
                RuleFor(x => x.EventId).NotEmpty();
            }
        }
    }
}
