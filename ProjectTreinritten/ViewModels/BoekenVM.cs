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
        public List<string> Namen { get; set; }
        public List<string> Voornamen { get; set; }
        [Required(ErrorMessage = "Gelieve een vertrekdatum op te geven")]
        [DataType(DataType.Date, ErrorMessage = "Gelieve een datum op te geven in formaat yyyy-mm-dd")]
        public string Vertrekdatum { get; set; }
        public int? HotelId { get; set; }
        public IEnumerable<Rit> Ritten { get; set; }
        public List<Traject> Trajecten { get; set; }
        public int GekozenTrajectId { get; set; }
        public string VertrekpuntNaam { get; set; }
        public string EindpuntNaam { get; set; }
        public TimeSpan Vertrekuur { get; set; }
        public TimeSpan  Aankomstuur { get; set; }
    }
}
