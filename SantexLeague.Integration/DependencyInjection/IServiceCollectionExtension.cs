
using System;
using System.Net.Http;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SantexLeague.Integration.HttpUtilities;
using SantexLeague.Integration.Services;

namespace SantexLeague.Integration.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddIntegrationLibrary(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHttpManager, HttpManager>();
            services.AddScoped<IImportLeagueService, ImportLeagueService>();
            return services;
        }
    }
}
