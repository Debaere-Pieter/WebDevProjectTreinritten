using ProjectTreinritten.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.ViewModels
{
    public class BoekenVM
    {
        [Required(ErrorMessage = "Gelieve een vertrekpunt op te geven")]
        public int Vertrekpunt { get; set; }
        [Required(ErrorMessage = "Gelieve een eindpunt op te geven")]
        public int Eindpunt { get; set; }
        [Required(ErrorMessage = "Gelieve een klasse op te geven")]
        public string Klasse { get; set; }
        [Required(ErrorMessage = "Gelieve een aantal personen op te geven")]
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
        [Required(ErrorMessage = "Gelieve een vertrekdatum op te geven")]
        public DateTime Vertrekdatum { get; set; }
        public int HotelNaam { get; set; }
        public IEnumerable<Rit> Ritten { get; set; }
    }
}
