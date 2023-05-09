using Carter;
using Carter.ModelBinding;
using EventTicketing.Application.Domain.Entities;
using EventTicketing.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventTicketing.Application.Features.Zones.Commands
{
    public class DeleteZone : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/Zone/{zoneId}", async (IMediator mediator, int zoneId) =>
            {
                return await mediator.Send(new DeleteZoneCommand() { ZoneId = zoneId });
            })
            .WithName(nameof(DeleteZone))
            .WithTags(nameof(Zone))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        }

        public class DeleteZoneCommand : IRequest<IResult>
        {
            public int ZoneId { get; set; }
        }

        public class DeleteZoneHandler : IRequestHandler<DeleteZoneCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<DeleteZoneCommand> _validator;

            public DeleteZoneHandler(EventTicketingContext context, IValidator<DeleteZoneCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(DeleteZoneCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var zone = await _context.Zones.FindAsync(request.ZoneId);
                if (zone is null)
                    return Results.NotFound($"No se encuentra una zona de id {request.ZoneId}");

                _context.Remove(zone);
                await _context.SaveChangesAsync();

                return Results.NoContent();
            }

            public class DeleteZoneValidator : AbstractValidator<DeleteZoneCommand>
            {
                public DeleteZoneValidator() => RuleFor(x => x.ZoneId).NotEmpty();
            }
        }
    }
}
