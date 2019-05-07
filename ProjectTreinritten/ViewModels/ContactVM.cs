using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTreinritten.ViewModels
{
    public class ContactVM
    {
        [Required(ErrorMessage = "Gelieve uw naam op te geven")]
        public string JouwNaam { get; set; }
        [Required(ErrorMessage = "Gelieve uw email-adres op te geven")]
        public string JouwEmail { get; set; }
        [Required(ErrorMessage = "Gelieve een boodschap in te geven")]
        public string  Message { get; set; }
    }
}
