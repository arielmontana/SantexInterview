using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SantexLeague.Domain;
using SantexLeague.Integration.ExternalEntities;

namespace SantextLeague.Tests.Utils
{
    public class AutoMapperFake
    {
        public static IMapper Get() 
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<TeamDto, Team>()
                .ForMember(domain => domain.Id, opt => opt.Ignore())
                .ForMember(domain => domain.AreaName, dto => dto.MapFrom(x => x.area.name))
                .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id));
                opts.CreateMap<CompetitionDto, Competition>()
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id))
                    .ForMember(domain => domain.AreaName, dto => dto.MapFrom(x => x.area.name));
                opts.CreateMap<TeamCompetitionItemDto, TeamCompetition>()
                    .ForMember(domain => domain.Competition, dto => dto.MapFrom(x => x.competition))
                    .ForMember(domain => domain.Team, dto => dto.MapFrom(x => x.team));
                opts.CreateMap<PlayerDto, Player>()
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.ExternalId, dto => dto.MapFrom(x => x.id));
                opts.CreateMap<PlayerTeamItemDto, PlayerTeam>();
            });
            return config.CreateMapper();
        }
    }
}
