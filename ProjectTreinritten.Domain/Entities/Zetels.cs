using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Zetels
    {
        public int ZetelId { get; set; }
        public int Rit1Zetel { get; set; }
        public int? Rit2Zetel { get; set; }
        public int? Rit3Zetel { get; set; }
        public int BoekingId { get; set; }

        public Boeking Boeking { get; set; }
    }
}
