using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.Domain.Base;

namespace SantexLeague.DataAccess.EntityFramework.Maps.Base
{
    public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            OnConfigure(builder);
        }

        public abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);
    }
}