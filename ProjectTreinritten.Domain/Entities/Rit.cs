using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Rit
    {
        public Rit()
        {
            TrajectRit1 = new HashSet<Traject>();
            TrajectRit2 = new HashSet<Traject>();
            TrajectRit3 = new HashSet<Traject>();
        }

        public int RitId { get; set; }
        public TimeSpan VertrekUur { get; set; }
        public TimeSpan AankomstUur { get; set; }
        public TimeSpan Duur { get; set; }
        public int VertrekStationId { get; set; }
        public int AankomstStationId { get; set; }
        public int TreinTypeId { get; set; }

        public Station AankomstStation { get; set; }
        public TreinType TreinType { get; set; }
        public Station VertrekStation { get; set; }
        public ICollection<Traject> TrajectRit1 { get; set; }
        public ICollection<Traject> TrajectRit2 { get; set; }
        public ICollection<Traject> TrajectRit3 { get; set; }
    }
}
