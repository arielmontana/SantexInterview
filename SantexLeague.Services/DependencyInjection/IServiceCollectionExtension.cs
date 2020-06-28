using System;
using Microsoft.Extensions.DependencyInjection;

namespace SantexLeague.Services.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServiceLibrary(this IServiceCollection services)
        {
            services.AddScoped<ICompetitionService, CompetitionService>();
            services.AddScoped<IPlayersService, PlayersService>();
            return services;
        }
    }
}