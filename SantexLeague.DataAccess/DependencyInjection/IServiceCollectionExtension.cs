using Microsoft.Extensions.DependencyInjection;
using SantexLeague.DataAccess.Repositories;
using SantexLeague.DataAccess.UnitsOfWork;

namespace SantexLeague.DataAccess.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataAccessLibrary(this IServiceCollection services)
        {
            services.AddScoped<ICompetitionRepository, CompetitionRepository>();
            services.AddScoped<IPlayersRepository, PlayersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}