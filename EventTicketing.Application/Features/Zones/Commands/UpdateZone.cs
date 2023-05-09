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
    public class UpdateZone : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/Zone", async (IMediator mediator, UpdateZoneCommand command) =>
            {
                return await mediator.Send(command);
            })
            .WithName(nameof(UpdateZone))
            .WithTags(nameof(Zone))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);
        }

        public class UpdateZoneCommand : IRequest<IResult>
        {
            public int ZoneId { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class UpdateZoneHandler : IRequestHandler<UpdateZoneCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<UpdateZoneCommand> _validator;

            public UpdateZoneHandler(EventTicketingContext context, IValidator<UpdateZoneCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(UpdateZoneCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var zone = await _context.Zones.FindAsync(request.ZoneId);
                if(zone is null)
                    return Results.NotFound($"No se encuentra una zona de id {request.ZoneId}");

                zone.UpdateZone(request);

                _context.Update(zone);
                await _context.SaveChangesAsync();

                return Results.NoContent();
            }
        }


        public class UpdateZoneValidator : AbstractValidator<UpdateZoneCommand>
        {
            public UpdateZoneValidator()
            {
                RuleFor(x => x.ZoneId).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        } 
    }
}
