using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.EntityFramework.Maps
{
    public class TeamCompetitionMap : BaseMap<TeamCompetition>
    {
        public override void OnConfigure(EntityTypeBuilder<TeamCompetition> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.CompetitionId);
            builder.HasOne(entity => entity.Competition)
                .WithMany(competition => competition.TeamsCompetition)
                .HasForeignKey(competition => competition.CompetitionId);
            builder.Property(entity => entity.TeamId);
            builder.HasOne(entity => entity.Team)
                .WithOne().HasForeignKey<TeamCompetition>(x => x.TeamId);
        }
    }
}