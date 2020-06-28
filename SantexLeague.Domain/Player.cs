using System;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain
{
    public class Player : BaseEntity
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBird { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}