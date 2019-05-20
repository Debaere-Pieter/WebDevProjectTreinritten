using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.ViewModel
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }

    }

    public class CartVM
    {
        public int TrajectNr { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public float Prijs { get; set; }
        public int Aantal { get; set; }
        public int? HotelId { get; set; }
        public string Klasse { get; set; }
        public string Vertrekdatum { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string VertrekPunt { get; set; }
        public string EindPunt { get; set; }
        public string Hotel { get; set; }
        public int? Zetel1 { get; set; }
        public int? Zetel2 { get; set; }
        public int? Zetel3 { get; set; }
      
    }
}
