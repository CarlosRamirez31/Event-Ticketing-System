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

namespace EventTicketing.Application.Features.Events.Queries
{
    public class GetEvent : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/Event", async (IMediator mediator) =>
            {
                return await mediator.Send(new GetEventQuery());
            })
            .WithName(nameof(GetEvent))
            .WithTags(nameof(Event));
            
        }

        public class GetEventQuery : IRequest<List<GetEventDto>>
        {

        }

        public class GetEventByIdHandler : IRequestHandler<GetEventQuery, List<GetEventDto>>
        {
            private readonly EventTicketingContext _context;
            private readonly IMapper _mapper;

            public GetEventByIdHandler(EventTicketingContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public Task<List<GetEventDto>> Handle(GetEventQuery request, CancellationToken cancellationToken) =>
                _context.Events.ProjectTo<GetEventDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public class GetEventMappingProfile : Profile
        {
            public GetEventMappingProfile() => CreateMap<Event, GetEventDto>();
        }

        public record GetEventDto(int EventId, string Name, DateTime StartDate, DateTime EndDate, string Location, string EventCreatedBy);
    }
}
