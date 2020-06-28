using System;
using AutoMapper;
using SantexLeague.Domain;
using SantexLeague.Integration.ExternalEntities;

namespace SantexLeague.Integration.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamDto, Team>()
                .ForMember(domain => domain.Id, opt => opt.Ignore())
                .ForMember(domain => domain.AreaName, dto => dto.MapFrom(x => x.area.name))
                .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id));
            CreateMap<CompetitionDto, Competition>()
                .ForMember(domain => domain.Id, opt => opt.Ignore())
                .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id))
                .ForMember(domain => domain.AreaName, dto => dto.MapFrom(x => x.area.name));
            CreateMap<TeamCompetitionItemDto, TeamCompetition>()
                .ForMember(domain => domain.Competition, dto => dto.MapFrom(x => x.competition))
                .ForMember(domain => domain.Team, dto => dto.MapFrom(x => x.team));
            CreateMap<PlayerDto, Player>()
                .ForMember(domain => domain.Id, opt => opt.Ignore())
                .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id));
            CreateMap<PlayerTeamItemDto, PlayerTeam>();
                //.ForMember(domain => domain.Players, dto => dto.MapFrom(x => x.squad));
        }
    }
}