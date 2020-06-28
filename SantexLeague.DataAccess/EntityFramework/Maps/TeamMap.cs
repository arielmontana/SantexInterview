using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.EntityFramework.Maps
{
    public class TeamMap : BaseMap<Team>
    {
        public override void OnConfigure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name).HasMaxLength(200);
            builder.Property(entity => entity.ShortName).HasMaxLength(50);
            builder.Property(entity => entity.AreaName).HasMaxLength(200);
            builder.Property(entity => entity.Tla).HasMaxLength(50);
            builder.Property(entity => entity.Email).HasMaxLength(200);
            builder.Property(entity => entity.ExternalId).IsRequired();
            
            builder.HasMany(entity => entity.PlayersTeam)
                .WithOne(playersTeam => playersTeam.Team)
                .HasForeignKey(playersTeam => playersTeam.TeamId);
        }
    }
}