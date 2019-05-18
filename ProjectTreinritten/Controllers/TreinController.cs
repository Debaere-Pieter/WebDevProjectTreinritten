using BierSQLIdentity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Extensions;
using ProjectTreinritten.Service;
using ProjectTreinritten.ViewModel;
using ProjectTreinritten.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectTreinritten.Controllers
{
    public class TreinController : Controller
    {
        private BoekingService boekingService;
        private HotelService hotelService;
        private RitService ritService;
        private StationService stationService;
        private TrajectService trajectService;
        private TreinTypeService treinTypeService;
        private VakantieDagenService vakantieDagenService;

        public TreinController()
        {
            boekingService = new BoekingService();
            hotelService = new HotelService();
            ritService = new RitService();
            stationService = new StationService();
            trajectService = new TrajectService();
            treinTypeService = new TreinTypeService();
            vakantieDagenService = new VakantieDagenService();
        }

        //todo's vragen voor de prijs en vragen of we moeten checken of ticketten uniek moeten zijn.
        //overstappen hardcoden of computer laten zoeken
        //namen pieter jan navragen
        //script bieridentity
        //wisselknop doen
        


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
            
            ViewBag.StationLijst =
                new SelectList(stationService.GetAll(),
                 "StationId", "StationNaam");

            
            ViewBag.HotelLijst =
                new SelectList(hotelService.GetAll(),
                 "StationId", "HotelNaam");

            return View();
        }

        [HttpPost]
        public IActionResult KiezenRit(BoekenVM b)
        {
            if (b == null)
            {
                return NotFound();
            }

            //System.Diagnostics.Debug.WriteLine("printen in console");

            if(b.Eindpunt == b.Vertrekpunt)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Eindpunt en vertrekpunt mogen niet dezelfde waarde hebben");
            }
            if (b.Eindpunt == 0)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Gelieve een eindpunt te selecteren");
            }
            if (b.Vertrekpunt == 0)
            {
                ModelState.AddModelError(nameof(b.Eindpunt), "Gelieve een eindpunt te selecteren");
            }

            if (b.Vertrekdatum.Equals(""))
            {
                ModelState.AddModelError(nameof(b.Vertrekdatum), "Gelieve een datum op te geven in formaat yyyy - mm - dd");
            }

            for (int a = 0; a < b.AantalPersonen; a++)
            {
                if (string.IsNullOrEmpty(b.Namen[a]) || b.Namen[a].Length < 3 || b.Namen[a].Length > 30)
                {
                    ModelState.AddModelError(nameof(b.Namen), "Gelieve een geldige naam voor persoon " + (a + 1) + " op te geven");
                }
                if (string.IsNullOrEmpty(b.Voornamen[a]) || b.Voornamen[a].Length < 3 || b.Voornamen[a].Length > 30)
                {
                    ModelState.AddModelError(nameof(b.Voornamen), "Gelieve een geldige voornaam voor persoon " + (a + 1) + " op te geven");
                }
            }

            if (ModelState.IsValid)
            {
                var vertrekpunt = b.Vertrekpunt;
                var aankomst = b.Eindpunt;


                var list = ritService.GetAllByCitiesWithDate(b.Vertrekpunt, b.Eindpunt, DateTime.Parse(b.Vertrekdatum));
                //als de lijst leeg is wil dit zeggen dat  de route uit meer dan één rit bestaat
                if (list.Count() == 0)
                {
                    var listVertrekRitten = ritService.GetAllByDepartCity(vertrekpunt);
                    //List<int> listVertrekRitten2 = null;
                    var listAankomstRitten = ritService.GetAllByArrivalCity(aankomst);
                    //List<int> listAankomstRitten2 = null;
                    Boolean gevonden = false;

                    foreach (var item in listVertrekRitten)
                    {
                        //listVertrekRitten2.Add(item.AankomstStationId);
                        foreach (var item2 in listAankomstRitten)
                        {
                            //listAankomstRitten2.Add(item.VertrekStationId);
                            if (item.AankomstStationId == item2.VertrekStationId)
                            {

                                Traject traject = new Traject
                                {
                                    Rit1Id = item.RitId,
                                    Rit2Id = item2.RitId
                                };

                                var trajectenList = new List<Traject>();
                                trajectenList.Add(traject);
                                b.Trajecten = trajectenList;

                                gevonden = true;
                                
                            }
                        }
                    }

                    if (!gevonden)
                    {
                        
                    }
                }
                else
                {
                    //traject bestaat uit één rit
                    Traject traject = new Traject
                    {
                        Rit1Id = list.ElementAt(0).RitId
                    };
                    var trajectenList = new List<Traject>();
                    trajectenList.Add(traject);
                    b.Trajecten = trajectenList;
                }

                return View(b);
            }
            else
            {
                
                ViewBag.StationLijst =
                    new SelectList(stationService.GetAll(),
                     "StationId", "StationNaam");

                
                ViewBag.HotelLijst =
                    new SelectList(hotelService.GetAll(),
                     "StationId", "HotelNaam");

                return View("Boeken", b);
            }
        }

        //pagina die bevestiging van boeking toont
        public IActionResult Bevestiging(BoekingVM b)
        {
            return View(b);
        }

        //pagina om bestelgeschiedenis te bekijken
        public IActionResult Historiek()
        {
            return View();
        }

        //pagina om winkelkar te bekijken
        public IActionResult Winkelkar()
        {
            return RedirectToAction("Index", "ShoppingCart");
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

        public IActionResult Select(BoekenVM b)
        {
            if (b == null)
            {
                return NotFound();
            }

            var id = b.GekozenRitId;
            Rit r = ritService.Get(id);
            var test = b.Namen;

            Traject traject = new Traject
            {
                Rit1Id = id
            };

            trajectService.Create(traject);

            Traject t = trajectService.GetByRit(r);

            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            for (var i = 0; i < b.Namen.Count(); i++)
            {
                if (b.Namen[i] != null)
                {
                    CartVM item = new CartVM
                    {
                        TrajectNr = t.TrajectId,
                        Naam = b.Namen.ElementAt(i),
                        Voornaam = b.Voornamen.ElementAt(i),
                        Prijs = 15,
                        Aantal = 1,
                        HotelId = b.HotelId,
                        Klasse = b.Klasse,
                        Vertrekdatum = b.Vertrekdatum,
                        DateCreated = DateTime.Now
                    };

                    shopping.Cart.Add(item);

                    HttpContext.Session.SetObject("ShoppingCart", shopping);
                }
            } 
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p> Email From: " + "{0} ({1}) </p> <p>Message:" +
                        "</p><p>{2}</p>";
                    body = string.Format(body, model.JouwNaam, model.JouwEmail, model.Message);

                    EmailSender mail = new EmailSender();
                    await mail.SendEmailAsync(model.JouwEmail, "contact", body);
                    return RedirectToAction("Sent");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public IActionResult Sent()
        {
            return View();
        }
    }
}