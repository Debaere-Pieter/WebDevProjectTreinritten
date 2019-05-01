using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Traject
    {
        public Traject()
        {
            Boeking = new HashSet<Boeking>();
        }

        public int TrajectId { get; set; }
        public int Rit1Id { get; set; }
        public int? Rit2Id { get; set; }
        public int? Rit3Id { get; set; }

        public Rit Rit1 { get; set; }
        public Rit Rit2 { get; set; }
        public Rit Rit3 { get; set; }
        public ICollection<Boeking> Boeking { get; set; }
    }
}
