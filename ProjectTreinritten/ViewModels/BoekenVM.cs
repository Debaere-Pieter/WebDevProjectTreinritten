using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.ViewModels
{
    public class BoekenVM
    {
        public int Vertrekpunt { get; set; }
        public int Eindpunt { get; set; }
        public string Klasse { get; set; }
        public int AantalPersonen { get; set; }
        public string Voornaam1 { get; set; }
        public string Naam1 { get; set; }
        public string Voornaam2 { get; set; }
        public string Naam2 { get; set; }
        public string Voornaam3 { get; set; }
        public string Naam3 { get; set; }
        public string Voornaam4 { get; set; }
        public string Naam4 { get; set; }
        public string Voornaam5 { get; set; }
        public string Naam5 { get; set; }
        public string Voornaam6 { get; set; }
        public string Naam6 { get; set; }
        public string Voornaam7 { get; set; }
        public string Naam7 { get; set; }
        public string Voornaam8 { get; set; }
        public string Naam8 { get; set; }
        public string Voornaam9 { get; set; }
        public string Naam9 { get; set; }
        public string Voornaam10 { get; set; }
        public string Naam10 { get; set; }
        public DateTime Vertrekdatum { get; set; }
        public int HotelNaam { get; set; }
        public List<Rit> Ritten { get; set; }
    }
}
