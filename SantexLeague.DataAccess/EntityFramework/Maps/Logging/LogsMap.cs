using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantexLeague.DataAccess.EntityFramework.Maps.Base;
using SantexLeague.Domain.Logging;

namespace SantexLeague.DataAccess.EntityFramework.Maps.Logging
{
    public class LogsMap : BaseMap<Logs>
    {
        public override void OnConfigure(EntityTypeBuilder<Logs> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Message);
            builder.Property(entity => entity.MessageTemplate);
            builder.Property(entity => entity.Level).HasMaxLength(128);
            builder.Property(entity => entity.TimeStamp).IsRequired();
            builder.Property(entity => entity.Exception);
            builder.Property(entity => entity.Properties);
            builder.Property(entity => entity.LogEvent);
        }
    }
}
