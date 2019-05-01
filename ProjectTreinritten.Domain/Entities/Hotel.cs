using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Hotel
    {
        public Hotel()
        {
            Boeking = new HashSet<Boeking>();
        }

        public int HotelId { get; set; }
        public string HotelNaam { get; set; }
        public int StationId { get; set; }

        public Station Station { get; set; }
        public ICollection<Boeking> Boeking { get; set; }
    }
}
