using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.EntityFramework.Maps
{
    public class PlayerTeamMap : BaseMap<PlayerTeam>
    {
        public override void OnConfigure(EntityTypeBuilder<PlayerTeam> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.TeamId);
            builder.HasOne(entity => entity.Team)
                .WithMany(team => team.PlayersTeam)
                .HasForeignKey(team => team.TeamId);
            builder.Property(entity => entity.PlayerId);
            builder.HasOne(entity => entity.Player)
                .WithOne().HasForeignKey<PlayerTeam>(x => x.PlayerId);
        }
    }
}