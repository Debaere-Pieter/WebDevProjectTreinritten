using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Station
    {
        public Station()
        {
            Hotel = new HashSet<Hotel>();
            RitAankomstStation = new HashSet<Rit>();
            RitVertrekStation = new HashSet<Rit>();
        }

        public int StationId { get; set; }
        public string StationNaam { get; set; }

        public ICollection<Hotel> Hotel { get; set; }
        public ICollection<Rit> RitAankomstStation { get; set; }
        public ICollection<Rit> RitVertrekStation { get; set; }
    }
}
