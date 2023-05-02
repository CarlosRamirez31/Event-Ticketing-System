using AutoMapper;
using AutoMapper.QueryableExtensions;
using Carter;
using EventTicketing.Application.Domain.Entities;
using EventTicketing.Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace EventTicketing.Application.Features.Zones.Queries
{
    public class GetZone : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/Zone", async (IMediator mediator) =>
            {
                return await mediator.Send(new GetZoneQuery());
            })
            .WithName(nameof(GetZone))
            .WithTags(nameof(Zone));
        }

        public class GetZoneQuery : IRequest<List<GetZoneDto>>
        {

        }

        public class GetZoneHandler : IRequestHandler<GetZoneQuery, List<GetZoneDto>>
        {
            private readonly EventTicketingContext _context;
            private readonly IMapper _mapper;

            public GetZoneHandler(EventTicketingContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public Task<List<GetZoneDto>> Handle(GetZoneQuery request, CancellationToken cancellationToken) =>
                _context.Zones.ProjectTo<GetZoneDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public class GetZoneMappingProfile : Profile
        {
            public GetZoneMappingProfile() => CreateMap<Zone, GetZoneDto>();
        }

        public record GetZoneDto(int ZoneId, string Name);
    }
}
