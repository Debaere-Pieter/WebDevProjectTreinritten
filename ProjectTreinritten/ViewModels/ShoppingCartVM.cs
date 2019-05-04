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
        public int BoekingNr { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public int Aantal { get; set; }
        public float Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
