using System;

namespace SantexLeague.Integration.ExternalEntities
{
    public class PlayerDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public DateTime dateOfBird { get; set; }
        public string countryOfBirth { get; set; }
        public string nationality { get; set; }
    }
}