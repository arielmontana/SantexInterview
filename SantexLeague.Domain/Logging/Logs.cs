using System;
using System.ComponentModel.DataAnnotations.Schema;
using SantexLeague.Domain.Base;

namespace SantexLeague.Domain.Logging
{
    public class Logs : BaseEntity
    {
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }

        [Column(TypeName = "xml")]
        public string Properties { get; set; }

        public string LogEvent { get; set; }
    }
}