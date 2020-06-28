using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain;

namespace SantexLeague.DataAccess.EntityFramework.Maps
{
    public class PlayerMap : BaseMap<Player>
    {
        public override void OnConfigure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name).HasMaxLength(200);
            builder.Property(entity => entity.Nationality).HasMaxLength(200);
            builder.Property(entity => entity.Position);
            builder.Property(entity => entity.DateOfBird);
            builder.Property(entity => entity.CountryOfBirth).HasMaxLength(200);
            builder.Property(entity => entity.ExternalId);
        }
    }
}