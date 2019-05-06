using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.ViewModels
{
    public class BoekingVM
    {
        public int BoekingId { get; set; }
        public string Klasse { get; set; }
        public DateTime BoekingsDatum { get; set; }
        public DateTime VertrekDatum { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int HotelId { get; set; }
        public int TrajectId { get; set; }
        public string LoginId { get; set; }
        public Hotel Hotel { get; set; }
        public AspNetUsers Login { get; set; }
        public Traject Traject { get; set; }
    }
}
