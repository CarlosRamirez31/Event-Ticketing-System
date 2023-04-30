using Carter;
using Carter.ModelBinding;
using EventTicketing.Application.Domain.Entities;
using EventTicketing.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations;

namespace EventTicketing.Application.Features.Zones.Commands
{
    public class CreateZone : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/Zone", async (IMediator mediator, CreateZoneCommand command) =>
            {
                return await mediator.Send(command);
            })
            .WithName(nameof(CreateZone))
            .WithTags(nameof(Zone))
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status201Created);
        }

        public class CreateZoneCommand : IRequest<IResult>
        {
            public string Name { get; set; } = string.Empty;
        }

        public class CreateZoneHandler : IRequestHandler<CreateZoneCommand, IResult>
        {
            private readonly EventTicketingContext _context;
            private readonly IValidator<CreateZoneCommand> _validator;

            public CreateZoneHandler(EventTicketingContext context, IValidator<CreateZoneCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
            {
                var result = _validator.Validate(request);
                if (!result.IsValid)
                    return Results.ValidationProblem(result.GetValidationProblems());

                var newZone = new Zone(0, request.Name);

                _context.Add(newZone);
                await _context.SaveChangesAsync();

                return Results.Created($"api/Zone/{newZone.ZoneId}", null);
            }
        }

        public class CreateZoneValidator : AbstractValidator<CreateZoneCommand>
        {
            public CreateZoneValidator() => RuleFor(x => x.Name).NotEmpty();
        }
    }
}
