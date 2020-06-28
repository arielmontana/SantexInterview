using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.EntityFramework.Maps
{
    public class CompetitionMap : BaseMap<Competition>
    {
        public override void OnConfigure(EntityTypeBuilder<Competition> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Code).HasMaxLength(4).IsRequired();
            builder.Property(entity => entity.AreaName).HasMaxLength(200);
            builder.Property(entity => entity.ExternalId).IsRequired();
            builder.Property(entity => entity.Name).HasMaxLength(200);
            builder.HasMany(entity => entity.TeamsCompetition)
                .WithOne(teamCompetition => teamCompetition.Competition)
                .HasForeignKey(teamCompetition => teamCompetition.CompetitionId);
        }
    }
}