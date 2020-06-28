using System;
using SantexLeague.Common;

namespace SantexLeague.Domain.Base
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}