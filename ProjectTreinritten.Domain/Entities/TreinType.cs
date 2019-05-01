using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class TreinType
    {
        public TreinType()
        {
            Rit = new HashSet<Rit>();
        }

        public int TreinTypeId { get; set; }
        public int CapaciteitBusiness { get; set; }
        public int CapaciteitEconomic { get; set; }

        public ICollection<Rit> Rit { get; set; }
    }
}
