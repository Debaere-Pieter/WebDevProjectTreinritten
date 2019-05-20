using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Service;

namespace ProjectTreinritten.Controllers
{
    public class HistoriekController : Controller
    {
        private BoekingService boekingService;
        public HistoriekController()
        {
            //nieuw boekingservice object aanmaken
            boekingService = new BoekingService();
        }

        public IActionResult Delete(int? boekingNr)
        {
            //boekingNr mag niet null zijn
            if (boekingNr == null)
            {
                return NotFound();
            }

            Boeking b = boekingService.Get((int)boekingNr);

            DateTime Vandaag = DateTime.UtcNow;
            DateTime DrieDagenVoorVertrek = b.VertrekDatum.AddDays(-3);

            //boeking verwijderen indien deze niet 3 dagen voor vertrek is.

            if (DateTime.Compare(Vandaag, DrieDagenVoorVertrek) < 0 )
            {                
                boekingService.Delete(b);
                return View("Succes");
            }
            else
            {
                return View("Mislukt");
            }
                
        }
    }
}