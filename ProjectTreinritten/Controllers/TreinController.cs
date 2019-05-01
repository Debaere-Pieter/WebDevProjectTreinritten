
using Microsoft.AspNetCore.Mvc;

namespace ProjectTreinritten.Controllers
{
    public class TreinController : Controller
    {
        //startpagina met linken naar alle functionaliteiten
        public IActionResult Index()
        {
            return View();
        }

        //pagina met alle informatie over het project: in opdracht van, door wie gemaakt etc.
        public IActionResult Over()
        {
            return View();
        }

        //pagina om een boeking te doen
        public IActionResult Boeken()
        {
            return View();
        }

        //pagina die bevestiging van boeking toont
        public IActionResult Bevestiging()
        {
            return View();
        }

        //pagina om bestelgeschiedenis te bekijken
        public IActionResult Historiek()
        {
            return View();
        }

        //pagina om winkelkar te bekijken
        public IActionResult Winkelkar()
        {
            return View();
        }

        //pagina om in te loggen
        public IActionResult Inloggen()
        {
            return View();
        }

        //pagina voor account aan te maken
        public IActionResult Registreer()
        {
            return View();
        }

        //404 pagina, deze pagina word getoond indien de pagina waarnaar men surft leeg is
        public IActionResult Leeg()
        {
            return View();
        }

        //pagina waar men wachtwoord kan opvragen via email indien men deze vergeten is
        public IActionResult Wachtwoord()
        {
            return View();
        }
    }
}