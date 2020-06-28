using Microsoft.EntityFrameworkCore;
using SantexLeague.Domain;
using SantexLeague.Domain.Logging;

namespace SantexLeague.DataAccess.EntityFramework
{
    public class SantexContext : DbContext
    {
        public SantexContext(DbContextOptions<SantexContext> options)
            : base(options)
        {
        }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<TeamCompetition> TeamCompetitions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PlayerTeam> PlayersTeam { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Logs> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}